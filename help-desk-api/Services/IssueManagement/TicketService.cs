using Models.AppSettings;
using Models.Dto.Ticket;
using Models.Entities.File;
using Models.Entities.Issue;
using Models.Entities.Org;
using Models.Enum;
using Repository;

namespace Services.IssueManagement
{
    public interface ITicketService
    {
        public Task<string> CreateTicket(AddTicketInputDto input);
    }
    public class TicketService : ITicketService
    {
        private readonly IUnitOfWork _unitOfWork;
        public TicketService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<string> CreateTicket(AddTicketInputDto input)
        {
            var ticketNumber = await GenerateNextTicketNumberAsync(input.FKCompanyId);
            var departments = await _unitOfWork.Repository<DepartmentModel, int>().FindByConditionAsync(d => d.RStatus == EnumRStatus.Active && input.FKDepartmentId.Contains(d.Id));
            departments.ToList();
            #region Ticket Creation
            TicketModel ticket = new TicketModel
            {
                TicketNumber = ticketNumber,
                Subject = input.Description,
                Description = "Open",
                SubmittedByUserId = 1, // ??
                FKCompanyId = input.FKCompanyId,
                RootCauseId = input.FkRootCauseId,
                ResolutionId = input.FkRelocationId,
                TicketCategory = EnumQMSType.Ticket, // default ticket category
                Status = EnumTicketStatus.Open,
                FKTicketTypeId = input.FkTicketTypeId,
                AssignedUserId = input.FKAssignUser,
                Priority = EnumPriority.Medium, // default priority from ticket type
                RStatus = EnumRStatus.Active,
                CreatedBy = 1, // ??
                CreatedDate = DateTime.UtcNow,
                Guid = Guid.NewGuid().ToString("N").Substring(0, 8),
                DueDate = DateTime.UtcNow, // todo: calculate due date based on ticket type and priority,
                EstimatedTime = "2h", // todo: get estimated time from ticket type and priority,

            };

            #endregion
            #region Department Map
            foreach (var deptId in input.FKDepartmentId)
            {
                TicketDepartmentMapModel departmentMap = new TicketDepartmentMapModel
                {
                    FKDepartmentId = deptId,
                    RStatus = EnumRStatus.Active,
                    CreatedBy = 1, // ??
                    CreatedDate = DateTime.UtcNow
                };
                ticket.DepartmentMaps.Add(departmentMap);
            }
            #endregion
            #region Custom field
            foreach (var customField in input.SubFrom)
            {
                TicketCustomFieldValue ticketCustomField = new TicketCustomFieldValue
                {
                    Value = customField.Value,
                    RStatus = EnumRStatus.Active,
                    TicketTypeCustomFieldId = customField.Id,
                    CreatedBy = 1, // ??
                    CreatedDate = DateTime.UtcNow

                };
                ticket.CustomFieldValues.Add(ticketCustomField);
            }
            #endregion
            #region Attachments
            var tempFiles = await GetAndMoveAttachments(input.Files.ToList(), ticket.TicketNumber);
            foreach (var file in tempFiles)
            {
                TicketAttachmentModel attachments = new TicketAttachmentModel
                {
                    FileName = file.Name,
                    FilePath = file.Path,
                    FileExtension = Path.GetExtension(file.Path),
                    CreatedBy = 1, // ??
                    CreatedDate = DateTime.UtcNow,
                };
                ticket.Attachments.Add(attachments);
            }
            #endregion
            #region WatchList
            // Supervisor and Assigned user should be added to watchlist by default
            foreach (var department in departments)
            {
                if (department.FKManagerId > 0)
                {
                    TicketWatchListModel assign = new TicketWatchListModel
                    {
                        FKUserId = input.FKAssignUser,
                        RStatus = EnumRStatus.Active,
                        CreatedBy = 1, // ??
                        CreatedDate = DateTime.UtcNow
                    };
                    ticket.WatchList.Add(assign);
                }
            }
            TicketWatchListModel watchListModel = new TicketWatchListModel
            {
                FKUserId = input.FKAssignUser,
                RStatus = EnumRStatus.Active
            };
            ticket.WatchList.Add(watchListModel);
            #endregion
            #region Customer/Lead Map
            if (input.IsCustomer && input.FKCustomerId.HasValue)
            {
                TicketCustomerMapModel customerMap = new TicketCustomerMapModel
                {
                    FkCustomerId = input.FKCustomerId.Value,
                    RStatus = EnumRStatus.Active,
                    CreatedBy = 1, // ??
                    CreatedDate = DateTime.UtcNow
                };
                ticket.TicketCustomerMaps.Add(customerMap);
            }
            else if (!input.IsCustomer && input.FKProjectId.HasValue)
            {

                TicketProjectMapModel projectMap = new TicketProjectMapModel
                {
                    FkProjectId = input.FKProjectId.Value,
                    RStatus = EnumRStatus.Active,
                    CreatedBy = 1, // ??
                    CreatedDate = DateTime.UtcNow
                };
                ticket.TicketProjectMaps.Add(projectMap);
            }
            #endregion
            return "";
        }
        private async Task<List<AttachmentDto>> GetAndMoveAttachments(
           List<int> attachments,
           string orderReferenceNumber
       )
        {
            string targetRootPath = Path.Combine(
                AppSettings.TicketPath,
                orderReferenceNumber
            );

            var movedFileNames = new List<AttachmentDto>();

            var attachFiles = await _unitOfWork.Repository<TempFileModel, int>().FindByConditionAsync(s => attachments.Contains(s.Id));

            if (attachFiles == null)
                return new List<AttachmentDto>();

            attachFiles = attachFiles.ToList();
            foreach (var attachFile in attachFiles)
            {
                // Use Path class to manipulate file and directory paths.
                string SourceFile = attachFile.FilePath;
                if (!Directory.Exists(targetRootPath))
                    Directory.CreateDirectory(targetRootPath);

                string FileName = $"{attachFile.FileName}";
                string TargetFile = Path.Combine(targetRootPath, FileName);
                if (SourceFile != TargetFile)
                {
                    System.IO.File.Copy(SourceFile, TargetFile, true);
                    movedFileNames.Add(new AttachmentDto()
                    {
                        Name = FileName,
                        Path = TargetFile
                    });
                }
                else
                {
                    movedFileNames.Add(new AttachmentDto()
                    {
                        Name = FileName,
                        Path = TargetFile
                    });
                }
            }
            if (attachFiles.Count() > 0)
            {
                foreach (var attachFile in attachFiles)
                {
                    if(attachFile == null)
                        continue;
                    await _unitOfWork.Repository<TempFileModel, int>().SoftDeleteAsync(attachFile);
                }
            }
            await _unitOfWork.CommitAsync();

            return movedFileNames;
        }

        public async Task<string> GenerateNextTicketNumberAsync(int companyId)
        {
            // 1️⃣ Fetch company (tracked entity so EF will save updates)
            var company = await _unitOfWork.Repository<CompanyModel, int>()
                .FirstOrDefaultAsync(c => c.Id == companyId);

            if (company == null)
                throw new Exception($"Company not found for ID: {companyId}");

            // 2️⃣ Generate next ticket number
            company.LastTicketNumber += 1;
            var nextNumber = company.LastTicketNumber;

            var ticketNumber = $"{company.PrefixTicket}-{nextNumber:D6}";

            // 3️⃣ Update company record
            _unitOfWork.Repository<CompanyModel, int>().Update(company);

            // 4️⃣ Persist changes in the same transaction
            await _unitOfWork.CommitAsync();

            // 5️⃣ Return ticket number
            return ticketNumber;
        }

    }
}

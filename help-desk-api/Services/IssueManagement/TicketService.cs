using Models.AppSettings;
using Models.Dto.Ticket;
using Models.Dto.Tickets;
using Models.Entities.File;
using Models.Entities.Issue;
using Models.Entities.Org;
using Models.Enum;
using Repository;
using Stripe;

namespace Services.IssueManagement
{
    public interface ITicketService
    {
        Task<string> CreateTicket(AddTicketInputDto input);

        Task<List<TicketListOutputView>> GetTicketLists(int companyId, TicketFilterInputDto input);
        TicketTileView GetTilesView(int companyId, TicketFilterInputDto input);


        #region Ticket Basic Details
        Task<TicketBasicDetailOutputDto> GetBasicDetails(int ticketId);
        Task<bool> UpdateBasicDetails(int ticketId, TicketBasicDetailInputDto input);
        #endregion
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
            try
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
                    FKCompanyId = 1,
                    RootCauseId = input.FkRootCauseId,
                    ResolutionId = input.FkResolutionId,
                    TicketCategory = EnumQMSType.Ticket, // default ticket category
                    Status = EnumTicketStatus.Open, // Its comFrom Ticket type
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
                await _unitOfWork.Repository<TicketModel, int>().AddAsync(ticket);
                await _unitOfWork.CommitAsync();
                #endregion
                #region Department Map
                foreach (var deptId in input.FKDepartmentId)
                {
                    TicketDepartmentMapModel departmentMap = new TicketDepartmentMapModel
                    {
                        FKDepartmentId = deptId,
                        RStatus = EnumRStatus.Active,
                        CreatedBy = 1, // ??
                        CreatedDate = DateTime.UtcNow,
                        FKTicketId = ticket.Id
                    };
                    await _unitOfWork.Repository<TicketDepartmentMapModel, int>().AddAsync(departmentMap);
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
                        CreatedDate = DateTime.UtcNow,
                        FkTicketId = ticket.Id

                    };

                    await _unitOfWork.Repository<TicketCustomFieldValue, int>().AddAsync(ticketCustomField);

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
                        FKTicketId = ticket.Id
                    };
                    await _unitOfWork.Repository<TicketAttachmentModel, int>().AddAsync(attachments);

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
                            CreatedDate = DateTime.UtcNow,
                            FKTicketId = ticket.Id
                        };
                        await _unitOfWork.Repository<TicketWatchListModel, int>().AddAsync(assign);

                    }
                }
                TicketWatchListModel watchListModel = new TicketWatchListModel
                {
                    FKUserId = input.FKAssignUser,
                    RStatus = EnumRStatus.Active
                };
                await _unitOfWork.Repository<TicketWatchListModel, int>().AddAsync(watchListModel);
                #endregion
                #region Customer/Lead Map
                if (input.IsCustomer && input.FKCustomerId.HasValue)
                {
                    TicketCustomerMapModel customerMap = new TicketCustomerMapModel
                    {
                        FkCustomerId = input.FKCustomerId.Value,
                        RStatus = EnumRStatus.Active,
                        CreatedBy = 1, // ??
                        CreatedDate = DateTime.UtcNow,
                        FKTicketId = ticket.Id
                    };
                    await _unitOfWork.Repository<TicketCustomerMapModel, int>().AddAsync(customerMap);
                }
                else if (!input.IsCustomer && input.FKProjectId.HasValue)
                {

                    TicketProjectMapModel projectMap = new TicketProjectMapModel
                    {
                        FkProjectId = input.FKProjectId.Value,
                        RStatus = EnumRStatus.Active,
                        CreatedBy = 1, // ??
                        CreatedDate = DateTime.UtcNow,
                        FKTicketId = ticket.Id
                    };
                    await _unitOfWork.Repository<TicketProjectMapModel, int>().AddAsync(projectMap);
                }
                #endregion
                return ticketNumber;
            }
            catch (Exception ex)
            {
                throw ex;
            }

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
                    if (attachFile == null)
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

        public async Task<TicketBasicDetailOutputDto> GetBasicDetails(int ticketId)
        {
            var ticket = await _unitOfWork.Repository<TicketModel, int>().FirstOrDefaultAsync(s => s.Id == ticketId);

            var ticketLinkingItems = _unitOfWork.Repository<TicketLinkModel, int>().FindByConditionSelected(s => s.RStatus == EnumRStatus.Active && s.FKTicketId == ticketId, x=> new ListTicketOutputDto { Id = x.Id, TicketNumber = x.Ticket.TicketNumber, Subject = x.Ticket.Subject, Description = x.Ticket.Description});

            var ticketView = new TicketBasicDetailOutputDto()
            {
                Company = new TicketCompanyViewDto()
                {
                    Id = ticket.FKCompanyId,
                    CompanyName = ticket.Company.Name
                },
                Description = ticket.Description,
                Id = ticket.Id,
                Subject = ticket.Subject,
                TicketNumber = ticket.TicketNumber,
                LinkingItems = ticketLinkingItems.ToList()
            };
            return ticketView;
        }

        public async Task<bool> UpdateBasicDetails(int ticketId, TicketBasicDetailInputDto input)
        {

            var ticket = await _unitOfWork.Repository<TicketModel, int>().FirstOrDefaultAsync(s => s.Id == ticketId);
            if (ticket == null)
                throw new Exception($"Ticket not found for Id");
            ticket.Description = input.Description;
            _unitOfWork.Repository<TicketModel, int>().Update(ticket);
            return await _unitOfWork.CommitAsync()>0;
        }
        public async Task<List<TicketListOutputView>> GetTicketLists(int companyId, TicketFilterInputDto input)
        {
            var ticket = await _unitOfWork.Repository<TicketModel, int>().FindByConditionAsync(s => s.RStatus == EnumRStatus.Active && s.FKCompanyId == companyId);


            var users = await _unitOfWork.Repository<Models.Entities.UserManagement.UserModel, int>().FindByConditionAsync(s => s.RStatus == EnumRStatus.Active && ticket.Select(x=> x.AssignedUserId).Contains(s.Id));



            var tickets = ticket.Select(s => new TicketListOutputView
            {
                Assignee = users.Where(x => x.Id == s.AssignedUserId).FirstOrDefault().FirstName,
                Reporter ="Saiful",
                CreatedDate = s.CreatedDate,
                Description = s.Description,
                Id = s.Id,
                LastUpdate = s.UpdatedDate.HasValue ? s.UpdatedDate.Value : s.CreatedDate,
                Title = s.Subject,
                TicketNumber = s.TicketNumber,
                Status = s.Status,
                Priority = s.Priority,
                Subject = s.Subject
            });
            return tickets.ToList();
        }

        public TicketTileView GetTilesView(int companyId, TicketFilterInputDto input)
        {
            var ticketTile = new TicketTileView()
            {
                ClosedTicket = 10,
                InProgressTicket = 10,
                OpenTicket = 10,
                TotalTicket = 30
            };
            return ticketTile;
        }
    }
}

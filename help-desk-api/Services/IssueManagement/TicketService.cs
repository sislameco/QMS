using Amazon.SimpleEmailV2.Model;
using Microsoft.AspNetCore.Components.Forms;
using Models.AppSettings;
using Models.Dto.Menus;
using Models.Dto.Org;
using Models.Dto.Ticket;
using Models.Dto.Tickets;
using Models.Entities.File;
using Models.Entities.Issue;
using Models.Entities.Org;
using Models.Enum;
using Repository;
using System.ComponentModel.Design;
using Utils;
using Utils.LoginData;

namespace Services.IssueManagement
{
    public interface ITicketService
    {
        Task<string> CreateTicket(AddTicketInputDto input);

        Task<List<TicketListOutputView>> GetTicketLists(int companyId, TicketFilterInputDto input);
        TicketTileView GetTilesView(int companyId, TicketFilterInputDto input);


        #region Ticket Basic Details
        Task<TicketBasicDetailOutputDto> GetBasicDetails(int ticketId);
        Task<TicketSpecificationOutputDto> GetSpecification(int ticketId);
        Task<List<FileDto>> GetAttachments(int ticketId);
        Task<List<TicketLinkingItemOutputDto>> GetLinkingItems(int ticketId);
        Task<List<TicketCommentOutputDto>> GetComments(int ticketId);
        Task<List<TicketFieldOutputDto>> GetDefineFields(int ticketId);
        Task<List<TicketWatchersOutputDto>> GetWatchers(int ticketId);
        Task<bool> AddWatcher(int ticketId,int userId);
        Task<bool>  DeleteWatcher(int id);
        Task<bool> UpdateBasicDetails(int ticketId, TicketBasicDetailInputDto input);
        Task<bool> UpdateSpecification(int ticketId, TicketSpecificationOutputDto input);
        Task<bool> UpdateDefineData(int ticketId,  List<UpdateSubFromInputDto> input  );
        #endregion


        #region Ticket Item Delete/Update SingleResponsibility
        Task<bool> DeleteWatcher(int ticketId, int watcherId);
        Task<bool> DeleteComment(int ticketId, int commentId);
        Task<bool> UpdateComment(int id, string comment);
        Task<bool>  AddComment(int ticketId, string comment);
        Task<bool> ChangeTicketStatus(int id, EnumTicketStatus status);
        #endregion


    }
    public class TicketService : ITicketService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserInfos _userInfo;
        public TicketService(IUnitOfWork unitOfWork, IUserInfos userInfo)
        {
            _unitOfWork = unitOfWork;
            _userInfo = userInfo;
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
                    Subject = input.Subject,
                    Description = input.Description,
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
                foreach (var customField in input.SubForm)
                {
                    TicketCustomFieldValueModel ticketCustomField = new TicketCustomFieldValueModel
                    {
                        Value = customField.Value,
                        RStatus = EnumRStatus.Active,
                        TicketTypeCustomFieldId = customField.Id,
                        CreatedBy = 1, // ??
                        CreatedDate = DateTime.UtcNow,
                        FkTicketId = ticket.Id

                    };

                    await _unitOfWork.Repository<TicketCustomFieldValueModel, int>().AddAsync(ticketCustomField);

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
                    await _unitOfWork.CommitAsync();
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



        public async Task<List<TicketListOutputView>> GetTicketLists(int companyId, TicketFilterInputDto input)
        {
            var ticket = await _unitOfWork.Repository<TicketModel, int>().FindByConditionAsync(s => s.RStatus == EnumRStatus.Active && s.FKCompanyId == companyId);


            var users = await _unitOfWork.Repository<Models.Entities.UserManagement.UserModel, int>().FindByConditionAsync(s => s.RStatus == EnumRStatus.Active && ticket.Select(x => x.AssignedUserId).Contains(s.Id));



            var tickets = ticket.Select(s => new TicketListOutputView
            {
                Assignee = users.Where(x => x.Id == s.AssignedUserId).FirstOrDefault().FirstName,
                Reporter = "Saiful",
                CreatedDate = s.CreatedDate,
                Description = s.Description,
                Id = s.Id,
                LastUpdate = s.UpdatedDate.HasValue ? s.UpdatedDate.Value : s.CreatedDate,
                Title = s.Subject,
                TicketNumber = s.TicketNumber,
                Status = s.Status,
                Priority = s.Priority,
                Subject = s.Subject,
                FkTicketTypeId = s.FKTicketTypeId,
            });
            int skip = (input.PageNo - 1) * input.ItemsPerPage;
            return tickets.Skip(skip).Take(input.ItemsPerPage).ToList();
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
        public async Task<TicketBasicDetailOutputDto> GetBasicDetails(int ticketId)
        {
            var ticket = await _unitOfWork.Repository<TicketModel, int>().FirstOrDefaultAsync(s => s.Id == ticketId);
            var company = await _unitOfWork.Repository<CompanyModel, int>().FirstOrDefaultAsync(s => s.Id == ticket.FKCompanyId);
            var ticketView = new TicketBasicDetailOutputDto()
            {
                Company = new TicketCompanyViewDto()
                {
                    Id = company.Id,
                    CompanyName = company.Name
                },
                Description = ticket.Description,
                Id = ticket.Id,
                Subject = ticket.Subject,
                TicketNumber = ticket.TicketNumber,
                Status = ticket.Status,
                Priority = ticket.Priority
               
            };
            return ticketView;
        }
        public async Task<TicketSpecificationOutputDto> GetSpecification(int ticketId)
        {
            var ticket = await _unitOfWork.Repository<TicketModel, int>().FirstOrDefaultAsync(s => s.Id == ticketId);
            return new TicketSpecificationOutputDto()
            {
                AssigneeId = ticket.AssignedUserId,
                DepartmentIds = ticket.DepartmentMaps.Select(s => s.FKDepartmentId).ToList(),
                ResolutionId = ticket.ResolutionId,
                RootCauseId = ticket.RootCauseId,
                FkTicketTypeId = ticket.FKTicketTypeId
            };
        }
        public async Task<List<FileDto>> GetAttachments(int ticketId)
        {
            var attachemnts = _unitOfWork.Repository<TicketAttachmentModel, int>().FindByConditionSelected(s => s.FKTicketId == ticketId, x => new FileDto
            {
                Id = x.Id,
                FileName = x.FileName,
                FilePath = Common.PreparePdfPath(x.FilePath),
                AddedBy = "Saiful",
                AddedOn = "saiful"
            });

            return attachemnts;
        }
        public async Task<List<TicketLinkingItemOutputDto>> GetLinkingItems(int ticketId)
        {
            var linkings = _unitOfWork.Repository<TicketLinkModel, int>().FindByConditionSelected(s => s.FKTicketId == ticketId, x => new TicketLinkingItemOutputDto
            {
                Id = x.Id,
                TicketNumber = "",
                Subject = "",

            });

            return linkings;
        }
        public async Task<List<TicketCommentOutputDto>> GetComments(int ticketId)
        {
            var comments = _unitOfWork.Repository<TicketCommentModel, int>().FindByConditionSelected(s => s.TicketId == ticketId, x => new TicketCommentOutputDto
            {
                Id = x.Id,
                CommentText = x.CommentText,
                CommentedBy = "",// 
                CommentedOn = DateTime.UtcNow, // 
                FkUserId = 1 ///
            });

            return comments;
        }
        public async Task<List<TicketFieldOutputDto>> GetDefineFields(int ticketId)
        {
            var ticket = await _unitOfWork.Repository<TicketModel, int>().FirstOrDefaultAsync(s => s.Id == ticketId);
            var fields = _unitOfWork.Repository<TicketCustomFieldValueModel, int>().FindByConditionSelected(s => s.FkTicketId == ticketId, x => new TicketFieldOutputDto
            {
                Id = x.Id,
                FkTicketTypeId = ticket.FKTicketTypeId,
                FkCustomeFieldId = x.TicketTypeCustomFieldId,
                Value = x.Value
            });
            return fields;

        }
        public async Task<List<TicketWatchersOutputDto>> GetWatchers(int ticketId)
        {
            var watchers = _unitOfWork.Repository<TicketWatchListModel, int>().FindByConditionSelected(s => s.RStatus == EnumRStatus.Active && s.FKTicketId == ticketId, x => new TicketWatchersOutputDto
            {
                Id = x.Id,
                FkUserId = x.FKUserId,
                AddedBy = "", // 
                AddedOn = DateTime.UtcNow // 
            });
            return watchers;
        }
        public async Task<bool> AddWatcher(int ticketId, int userId)
        {
            TicketWatchListModel watchListModel = new TicketWatchListModel
            {
                FKUserId = userId,
                RStatus = EnumRStatus.Active,
                CreatedBy = _userInfo.GetCurrentUserId(),
                CreatedDate = DateTime.UtcNow,
                FKTicketId = ticketId
            };
            await _unitOfWork.Repository<TicketWatchListModel, int>().AddAsync(watchListModel);
            return await _unitOfWork.CommitAsync() > 0;
        }
        public async Task<bool> DeleteWatcher(int id)
        {
            var watcher = await _unitOfWork.Repository<TicketWatchListModel, int>()
                .FirstOrDefaultAsync(s => s.RStatus == EnumRStatus.Active && s.Id == id);
            if (watcher == null)
                throw new Exception($"Watcher not found for Id");
            await _unitOfWork.Repository<TicketWatchListModel, int>().SoftDeleteAsync(watcher);
            return await _unitOfWork.CommitAsync() > 0;
        }
        public async Task<bool> UpdateBasicDetails(int ticketId, TicketBasicDetailInputDto input)
        {

            var ticket = await _unitOfWork.Repository<TicketModel, int>().FirstOrDefaultAsync(s => s.Id == ticketId);
            if (ticket == null)
                throw new Exception($"Ticket not found for Id");
            ticket.Description = input.Description;
            _unitOfWork.Repository<TicketModel, int>().Update(ticket);
            return await _unitOfWork.CommitAsync() > 0;
        }
        public async Task<bool> UpdateSpecification(int ticketId, TicketSpecificationOutputDto input)
        {
            var ticket = await _unitOfWork.Repository<TicketModel, int>().FirstOrDefaultAsync(s => s.Id == ticketId);
            if (ticket == null)
                throw new Exception($"Ticket not found for Id");

            ticket.RootCauseId = input.RootCauseId;
            ticket.ResolutionId = input.ResolutionId;
            ticket.AssignedUserId = input.AssigneeId;

            //ticket.DepartmentMaps = new List<TicketDepartmentMapModel>();

            _unitOfWork.Repository<TicketModel, int>().Update(ticket);
            return await _unitOfWork.CommitAsync() > 0;
        }









        #region Ticket Item Delete
        public async Task<bool> DeleteWatcher(int ticketId, int watcherId)
        {
            var watcher = await _unitOfWork.Repository<TicketWatchListModel, int>()
                .FirstOrDefaultAsync(s => s.RStatus == EnumRStatus.Active && s.FKTicketId == ticketId && s.Id == watcherId);
            if (watcher == null)
                throw new Exception($"Watcher not found for Id");

            await _unitOfWork.Repository<TicketWatchListModel, int>().SoftDeleteAsync(watcher);
            return await _unitOfWork.CommitAsync() > 0;
        }
        public async Task<bool>  AddComment(int ticketId, string comment)
        {
            TicketCommentModel ticketComment = new TicketCommentModel
            {
                CommentText = comment,
                RStatus = EnumRStatus.Active,
                CreatedBy = _userInfo.GetCurrentUserId(),
                CreatedDate = DateTime.UtcNow,
                TicketId = ticketId,
            };
            await _unitOfWork.Repository<TicketCommentModel, int>().AddAsync(ticketComment);
            return await _unitOfWork.CommitAsync() > 0;
        }
        public async Task<bool> DeleteComment(int ticketId, int commentId)
        {
            var comment = await _unitOfWork.Repository<TicketCommentModel, int>()
                .FirstOrDefaultAsync(s => s.RStatus == EnumRStatus.Active && s.TicketId == ticketId && s.Id == commentId);
            if (comment == null)
                throw new Exception($"comment not found for Id");
            await _unitOfWork.Repository<TicketCommentModel, int>().SoftDeleteAsync(comment);
            return await _unitOfWork.CommitAsync()> 0;
        }
        public async Task<bool> UpdateComment(int id, string comment)
        {
            var updateComment = await _unitOfWork.Repository<TicketCommentModel, int>()
            .FirstOrDefaultAsync(s => s.RStatus == EnumRStatus.Active && s.Id == id);

            if(comment == null)
               throw new BadRequestException($"comment not found for Id");

            _unitOfWork.Repository<TicketCommentModel, int>().Update(updateComment);
            return await _unitOfWork.CommitAsync() > 0;
        }
        public async Task<bool> ChangeTicketStatus(int ticketId, EnumTicketStatus status)
        {
            var ticket = await _unitOfWork.Repository<TicketModel, int>().FirstOrDefaultAsync(s => s.Id == ticketId);
            if (ticket == null)
                throw new Exception($"Ticket not found for Id");
            ticket.Status = status;
            _unitOfWork.Repository<TicketModel, int>().Update(ticket);
            return await _unitOfWork.CommitAsync() > 0;
        }
        public async Task<bool> UpdateDefineData(int ticketId, List<UpdateSubFromInputDto> input)
        {
            var fields = await _unitOfWork.Repository<TicketCustomFieldValueModel, int>().FindByConditionAsync(s => s.FkTicketId == ticketId && input.Select(s=> s.Id).Contains(s.Id));
            
            foreach (var item in input)
            {
                var field = fields.Where(s => s.Id == item.Id && s.TicketTypeCustomFieldId == item.FkCustomField).FirstOrDefault();

                if (field != null)
                {
                    field.Value = item.Value;
                    _unitOfWork.Repository<TicketCustomFieldValueModel, int>().Update(field);
                }
                else
                {
                     TicketCustomFieldValueModel ticketCustomField = new TicketCustomFieldValueModel
                    {
                        Value = item.Value,
                        RStatus = EnumRStatus.Active,
                        TicketTypeCustomFieldId = item.FkCustomField,
                        CreatedBy = 1, // ??
                        CreatedDate = DateTime.UtcNow,
                        FkTicketId = ticketId
                    };
                    await _unitOfWork.Repository<TicketCustomFieldValueModel, int>().AddAsync(ticketCustomField);
                }

            }
            return await _unitOfWork.CommitAsync() > 0;

        }


        #endregion
    }
}

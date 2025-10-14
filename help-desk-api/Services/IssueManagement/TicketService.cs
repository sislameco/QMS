using Models.Dto.Ticket;
using Models.Entities.File;
using Models.Entities.Issue;
using Models.Entities.Org;
using Models.Enum;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            var departments = await _unitOfWork.Repository<DepartmentModel, int>().FindByConditionAsync(d => d.RStatus == EnumRStatus.Active && input.FKDepartmentId.Contains(d.Id));
            departments.ToList();

            #region Ticket Creation
            TicketModel ticket = new TicketModel
            {
                TicketNumber = "", // generate ticket number
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
            var tempFiles = await _unitOfWork.Repository<TempFileModel, int>().FindByConditionAsync(t => t.RStatus == EnumRStatus.Active && input.Files.Contains(t.Id));
            tempFiles.ToList();
            foreach (var file in input.Files)
            {
                var tempFile = tempFiles.FirstOrDefault(t => t.Id == file);
                TicketAttachmentModel attachments = new TicketAttachmentModel
                {
                    FileName = tempFile.FileName,
                    FilePath = tempFile.FilePath,
                    FileExtension = tempFile.Extension,
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

            // get Ticket Id 

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
            else if(!input.IsCustomer && input.FKProjectId.HasValue)
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
    }
}

using Models.Dto.Ticket;
using Models.Entities.Issue;
using Models.Entities.Org;
using Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.IssueManagement
{
    public interface ITicketService
    {
        public string CreateTicket(AddTicketInputDto input);
    }
    internal class TicketService : ITicketService
    {
        public string CreateTicket(AddTicketInputDto input)
        {
            TicketModel ticket = new TicketModel
            {
                TicketNumber = "", // generate ticket number
                Subject = input.Description,
                Description = "Open",
                SubmittedByUserId = 1, // ??
                FKCompanyId = input.FKCompanyId, 
                TicketCategory = EnumQMSType.Ticket, // default ticket category
                Status = EnumTicketStatus.Open,
                FKTicketTypeId = input.FkTicketTypeId,
                AssignedUserId = input.FKAssignUser,
                Priority = EnumPriority.Medium, // default priority from ticket type
                RStatus = EnumRStatus.Active,



            };
            foreach(var customField in input.SubFrom)
            {
                TicketCustomFieldValue ticketCustomField = new TicketCustomFieldValue
                {
                    Value = customField.Value,
                    RStatus = EnumRStatus.Active,
                    TicketTypeCustomFieldId = customField.Id

                };
                ticket.CustomFieldValues.Add(ticketCustomField);
            }

            foreach (var file in input.Files)
            {
                TicketAttachmentModel attachments = new TicketAttachmentModel
                {
                    FileName = file.FileName,
                    FilePath = file.FilePath,
                     
                };
                ticket.Attachments.Add(attachments);
            }
            // waching list
            TicketWatchListModel assign = new TicketWatchListModel
            {
                FKUserId = input.FKAssignUser,
                RStatus = EnumRStatus.Active
            };
            ticket.WatchList.Add(assign);

            TicketWatchListModel watchListModel = new TicketWatchListModel
            {
                FKUserId = input.FKAssignUser,
                RStatus = EnumRStatus.Active
            };
            // get Ticket Id 

            ticket.WatchList.Add(watchListModel);


        }
    }
}

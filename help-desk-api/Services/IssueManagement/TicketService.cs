using Models.Dto.Ticket;
using Models.Entities.Issue;
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
                TicketCategory = EnumQMSType.Ticket,
                Status = EnumTicketStatus.Open,
                FKTicketTypeId = input.FkTicketTypeId,
                AssignedUserId = input.FKAssignUser,
                Priority = EnumPriority.Medium, // default priority from ticket type
                 RStatus = EnumRStatus.Active,
                   


            };
        }
    }
}

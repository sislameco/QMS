using Models.Dto.GlobalDto;
using Models.Enum;
using System.Security.Cryptography.X509Certificates;

namespace Models.Dto.Ticket
{
    public class TicketFilterInputDto: PagedInputDto
    {
        public int[] TicketType { get; set; }
        public EnumTicketStatus[] TicketStatus { get; set; }
        public int[] Priority { get; set; }
        public int[] AssignedTo { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }
}

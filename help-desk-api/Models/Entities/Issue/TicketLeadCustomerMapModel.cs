using Models.Entities.Org;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entities.Issue
{
    [Table("TicketLeadCustomerMap", Schema = "issue")]
    public class TicketLeadCustomerMapModel : BaseEntity<int>
    {

        public int FKTicketId { get; set; }
        public int FKLeadCustomerId { get; set; }

        [ForeignKey("FKTicketId")]
        public TicketModel Ticket { get; set; }

        [ForeignKey("FKLeadCustomerId")]
        public LeadCustomerModel Project { get; set; }
    }
}
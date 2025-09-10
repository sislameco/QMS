using Models.Entities.Org;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entities.Issue
{
    [Table("TicketLeadCustomerMap", Schema = "issue")]
    public class TicketLeadCustomerMapModel : BaseEntity<long>
    {

        public long FKTicketId { get; set; }
        public long FKLeadCustomerId { get; set; }

        [ForeignKey("FKTicketId")]
        public TicketModel Ticket { get; set; }

        [ForeignKey("FKLeadCustomerId")]
        public LeadCustomerModel Project { get; set; }
    }
}
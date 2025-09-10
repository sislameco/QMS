using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entities.Issue
{
    [Table("TicketLeadCustomerMap", Schema = "issue")]
    public class TicketLeadCustomerMapModel : BaseEntity<long>
    {
        public long TicketId { get; set; }
        public long LeadCustomerId { get; set; }
    }
}
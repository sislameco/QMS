using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entities.Company
{
    [Table("TicketLink", Schema = "company")]
    public class TicketLinkModel : BaseEntity<long>
    {
        public long TicketId { get; set; }
    }
}
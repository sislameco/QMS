using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entities.Company
{
    [Table("TicketAttachment", Schema = "company")]
    public class TicketAttachmentModel : BaseEntity<long>
    {
        public long TicketId { get; set; }
    }
}
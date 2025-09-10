using Models.Entities.Org;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entities.Issue
{
    [Table("TicketAttachment", Schema = "issue")]
    public class TicketAttachmentModel : BaseEntity<long>
    {
        public long FKTicketId { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string ContentType { get; set; }
        [ForeignKey("FKTicketId")]
        public TicketModel Ticket { get; set; }
    }
}
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entities.Issue
{
    [Table("TicketAttachment", Schema = "issue")]
    public class TicketAttachmentModel : BaseEntity<long>
    {
        public long TicketId { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string ContentType { get; set; }
        public long SizeBytes { get; set; }
        public long UploadedByUserId { get; set; }
        public DateTime UploadedAt { get; set; }
    }
}
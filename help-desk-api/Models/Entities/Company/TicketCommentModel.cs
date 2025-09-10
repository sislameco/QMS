using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entities.Company
{
    [Table("TicketComment", Schema = "company")]
    public class TicketCommentModel : BaseEntity<long>
    {
        public long TicketId { get; set; }
        public string CommentText { get; set; }
        public long CreatedByUserId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
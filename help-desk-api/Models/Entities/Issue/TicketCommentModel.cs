using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entities.Issue
{
    [Table("TicketComment", Schema = "issue")]
    public class TicketCommentModel : BaseEntity<long>
    {
        public long TicketId { get; set; }
        public string CommentText { get; set; }
        public long CreatedByUserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string MentionUserIds { get; set; }
    }
}
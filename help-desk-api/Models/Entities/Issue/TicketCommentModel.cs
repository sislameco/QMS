using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entities.Issue
{
    [Table("TicketComment", Schema = "issue")]
    public class TicketCommentModel : BaseEntity<int>
    {
        public int TicketId { get; set; }
        public string CommentText { get; set; }
        public int[] MentionUserIds { get; set; }
    }
}
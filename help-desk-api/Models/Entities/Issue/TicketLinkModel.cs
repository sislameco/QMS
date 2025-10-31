using Models.Entities.UserManagement;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entities.Issue
{
    [Table("TicketLink", Schema = "issue")]
    public class TicketLinkModel : BaseEntity<int>
    {
        public int FKTicketId { get; set; }
        public int LinkingTicketId { get; set; }
        public string ExternalKey { get; set; }
        public string Notes { get; set; }
        public int? FkUserId { get; set; }

        [ForeignKey("FkUserId")]
        public UserModel User { get; set; }

        [ForeignKey("FKTicketId")]
        public TicketModel Ticket { get; set; }
    }

}
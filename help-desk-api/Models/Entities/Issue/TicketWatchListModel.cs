using Models.Entities.UserManagement;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entities.Issue
{
    [Table("TicketWatchList", Schema = "issue")]
    public class TicketWatchListModel : BaseEntity<long>
    {
        public long FKTicketId { get; set; }
        public long FKUserId { get; set; }

        [ForeignKey("FKTicketId")]
        public TicketModel Ticket { get; set; }

        [ForeignKey("FKUserId")]
        public UserModel User { get; set; }
    }
}
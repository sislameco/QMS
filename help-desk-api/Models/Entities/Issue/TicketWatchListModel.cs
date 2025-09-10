using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entities.Issue
{
    [Table("TicketWatchList", Schema = "issue")]
    public class TicketWatchListModel : BaseEntity<long>
    {
        public long TicketId { get; set; }
        public long UserId { get; set; }
    }
}
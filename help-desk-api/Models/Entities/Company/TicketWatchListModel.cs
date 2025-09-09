using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entities.Company
{
    [Table("TicketWatchList", Schema = "company")]
    public class TicketWatchListModel : BaseEntity<long>
    {
        public long TicketId { get; set; }
        public long UserId { get; set; }
    }
}
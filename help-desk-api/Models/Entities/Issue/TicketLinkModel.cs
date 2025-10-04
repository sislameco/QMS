using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entities.Issue
{
    [Table("TicketLink", Schema = "issue")]
    public class TicketLinkModel : BaseEntity<int>
    {
        public int FKTicketId { get; set; }
        public string ExternalKey { get; set; }
        public string Notes { get; set; }

        [ForeignKey("FKTicketId")]
        public TicketModel Ticket { get; set; }
    }

}
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entities.Issue
{
    [Table("TicketLink", Schema = "issue")]
    public class TicketLinkModel : BaseEntity<long>
    {
        public long TicketId { get; set; }
        public LinkedEntityType LinkedEntityType { get; set; }
        public long? LinkedEntityId { get; set; }
        public string ExternalKey { get; set; }
        public string Notes { get; set; }
    }

    public enum LinkedEntityType { Ticket, Complaint, CAPA, Jira }
}
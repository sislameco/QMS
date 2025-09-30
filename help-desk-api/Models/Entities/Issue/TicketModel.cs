using Models.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entities.Issue
{
    [Table("Ticket", Schema = "issue")]
    public class TicketModel : BaseEntity<int>
    {
        public string TicketNumber { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public int SubmittedByUserId { get; set; }
        public int FKCompanyId { get; set; }
        public int FKTicketTypeId { get; set; }
        public QMSType TicketCategory { get; set; }
        public TicketStatus Status { get; set; }
        public TicketPriority Priority { get; set; }
        public int? AssignedUserId { get; set; }
        public int? RootCauseId { get; set; }
        public int? ResolutionId { get; set; }
        public string EstimatedTime { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? ResolvedAt { get; set; }
        [ForeignKey("FKCompanyId")]
        public TicketModel Ticket { get; set; }
        [ForeignKey("FKTicketTypeId")]
        public TicketTypeModel TicketType { get; set; }
        public ICollection<TicketAttachmentModel> Attachments { get; set; }
        public ICollection<TicketCommentModel> Comments { get; set; }
        public ICollection<TicketWatchListModel> WatchList { get; set; }
        public ICollection<TicketDepartmentMapModel> DepartmentMaps { get; set; }
        public ICollection<TicketLinkModel> Links { get; set; }
        public ICollection<TicketLeadCustomerMapModel> LeadCustomerMaps { get; set; }
    }

}
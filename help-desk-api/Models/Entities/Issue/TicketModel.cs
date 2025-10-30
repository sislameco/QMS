using Models.Entities.Org;
using Models.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entities.Issue
{
    [Table("Ticket", Schema = "issue")]
    public class TicketModel : BaseEntity<int>
    {
        public string Guid { get; set; }
        public string TicketNumber { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public int SubmittedByUserId { get; set; }
        public int FKCompanyId { get; set; }
        public int FKTicketTypeId { get; set; }
        public EnumQMSType TicketCategory { get; set; }
        public EnumTicketStatus Status { get; set; }
        public EnumPriority Priority { get; set; }
        public int? AssignedUserId { get; set; }
        public int? RootCauseId { get; set; }
        public int? ResolutionId { get; set; }
        public string EstimatedTime { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? ResolvedAt { get; set; }
        [ForeignKey("FKCompanyId")]
        public CompanyModel Company { get; set; }
        [ForeignKey("FKTicketTypeId")]
        public TicketTypeModel TicketType { get; set; }
        public ICollection<TicketAttachmentModel> Attachments { get; set; }
        public ICollection<TicketCommentModel> Comments { get; set; }
        public ICollection<TicketWatchListModel> WatchList { get; set; }
        public ICollection<TicketDepartmentMapModel> DepartmentMaps { get; set; }
        public ICollection<TicketLinkModel> Links { get; set; }
        public ICollection<TicketCustomerMapModel> TicketCustomerMaps { get; set; }
        public ICollection<TicketCustomFieldValueModel> CustomFieldValues { get; set; }
        public ICollection<TicketProjectMapModel> TicketProjectMaps { get; set; }
    }

}
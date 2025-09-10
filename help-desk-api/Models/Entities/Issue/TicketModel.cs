using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entities.Issue
{
    [Table("Ticket", Schema = "issue")]
    public class TicketModel : BaseEntity<long>
    {
        public long CompanyId { get; set; }
        public string TicketNumber { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public long SubmittedByUserId { get; set; }
        public long TicketTypeId { get; set; }
        public TicketCategory TicketCategory { get; set; }
        public TicketStatus Status { get; set; }
        public TicketPriority Priority { get; set; }
        public long? AssignedUserId { get; set; }
        public long? RootCauseId { get; set; }
        public long? ResolutionId { get; set; }
        public string EstimatedTime { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? ResolvedAt { get; set; }
        public ICollection<TicketAttachmentModel> Attachments { get; set; }
        public ICollection<TicketCommentModel> Comments { get; set; }
        public ICollection<TicketWatchListModel> WatchList { get; set; }
        public ICollection<TicketDepartmentMapModel> DepartmentMaps { get; set; }
        public ICollection<TicketLinkModel> Links { get; set; }
        public ICollection<TicketLeadCustomerMapModel> LeadCustomerMaps { get; set; }
    }

    public enum TicketCategory { Ticket, CAPA, Goals, Complaints }
    public enum TicketStatus { Open, InProgress, Resolved, Closed, Reopen }
    public enum TicketPriority { P1, P2, P3, P4 }
}
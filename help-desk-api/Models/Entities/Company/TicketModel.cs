using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Models.Entities.Org;
using Models.Entities.UserManagement;
using Models.Entities.Company;

namespace Models.Entities.Company
{
    [Table("Ticket", Schema = "company")]
    public class TicketModel : BaseEntity<long>
    {
        public long CompanyId { get; set; }
        public CompanyModel Company { get; set; }
        public string TicketNumber { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public long SubmittedByUserId { get; set; }
        public UserModel SubmittedByUser { get; set; }
        public long TicketTypeId { get; set; }
        public TicketTypeModel TicketType { get; set; }
        public TicketCategory Category { get; set; }
        public TicketStatus Status { get; set; }
        public TicketPriority Priority { get; set; }
        public long? AssignedUserId { get; set; }
        public UserModel AssignedUser { get; set; }
        public long? ProjectDirectoryId { get; set; }
        public ProjectDirectoryModel ProjectDirectory { get; set; }
        public long? RootCauseId { get; set; }
        public RootCauseModel RootCause { get; set; }
        public long? ResolutionId { get; set; }
        public ResolutionModel Resolution { get; set; }
        public string EstimatedTime { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? ResolvedAt { get; set; }
        public ICollection<TicketAttachmentModel> Attachments { get; set; }
        public ICollection<TicketCommentModel> Comments { get; set; }
        public ICollection<TicketWatchListModel> WatchList { get; set; }
        public ICollection<TicketDepartmentMapModel> DepartmentMaps { get; set; }
        public ICollection<TicketLinkModel> Links { get; set; }
    }

    public enum TicketCategory { Ticket, CAPA, Goals, Complaints }
    public enum TicketStatus { Open, InProgress, Resolved, Closed }
    public enum TicketPriority { P1, P2, P3, P4 }
}
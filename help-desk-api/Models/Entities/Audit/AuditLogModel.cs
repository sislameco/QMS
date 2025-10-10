using System;
using System.ComponentModel.DataAnnotations.Schema;
using Models.Entities.UserManagement;
using Models.Enum;

namespace Models.Entities.Audit
{
    [Table("AuditLog", Schema = "log")]
    public class AuditLogModel : BaseEntity<int>
    {
        public string EntityName { get; set; } // e.g., Ticket, Complaint, CAPA, User
        public int EntityId { get; set; } // PK of the entity being changed
        public AuditActionType ActionType { get; set; } // Created, Updated, etc.
        public string OldValues { get; set; } // JSON before change
        public string NewValues { get; set; } // JSON after change
        public DateTime ChangedDate { get; set; }
        public int ChangedBy { get; set; } // FK to User.Id
        public string IPAddress { get; set; }
        public string UserAgent { get; set; }
        public string Notes { get; set; }
        public UserModel User { get; set; } // Navigation property
    }

    [Table("HelpDeskAuditLog", Schema = "log")]
    public class QMSAuditLogModel
    {
        public int Id { get; set; }
        public int IntegratedCompanyId { get; set; }
        public string FkUserId { get; set; }
        public string IsHost { get; set; }
        public string Path { get; set; }
        public string Method { get; set; }
        public string RequestBody { get; set; }
        public string ResponseBody { get; set; }
        public int StatusCode { get; set; }
        public DateTime LogAt { get; set; }
    }
}
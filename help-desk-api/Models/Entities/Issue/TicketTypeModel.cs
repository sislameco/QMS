using Models.Entities.UserManagement;
using Models.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entities.Issue
{
    [Table("TicketType", Schema = "issue")]
    public class TicketTypeModel : BaseEntity<int>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsEnabled { get; set; }
        public EnumPriority Priority { get; set; }
        public int? FKAssignedUserId { get; set; }
        [ForeignKey("FKAssignedUserId")]
        public UserModel User { get; set; }
        public int?[] FKDepartmentIds { get; set; }
        public int FKCompanyId { get; set; }
        public EnumQMSType QmsType { get; set; }
    }


}
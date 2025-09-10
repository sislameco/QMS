using System.ComponentModel.DataAnnotations.Schema;
using Models.Entities;

namespace Models.Entities.UserManagement
{
    [Table("UserRole", Schema = "UserMgmt")]
    public class UserRoleModel : BaseEntity<long>
    {
        public long FKUserId { get; set; }
        public UserModel User { get; set; } = default!;
        public int FKRoleId { get; set; }
        public RoleModel Role { get; set; } = default!;
    }
}

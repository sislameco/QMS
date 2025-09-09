using System.ComponentModel.DataAnnotations.Schema;
using Models.Entities;

namespace Models.Entities.UserManagement
{
    [Table("UserRole", Schema = "UserMgmt")]
    public class UserRoleModel : BaseEntity<long>
    {
        public long UserId { get; set; }
        public UserModel User { get; set; } = default!;

        public int RoleId { get; set; }
        public RoleModel Role { get; set; } = default!;
    }
}

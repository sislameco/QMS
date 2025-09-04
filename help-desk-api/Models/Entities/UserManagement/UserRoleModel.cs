using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entities.UserManagement
{
    [Table("UserRole", Schema = "account")]
    public class UserRoleModel : BaseEntity<int>
    {
        public int UserId { get; set; }
        public UserModel User { get; set; } = default!;

        public int RoleId { get; set; }
        public RoleModel Role { get; set; } = default!;
    }
}

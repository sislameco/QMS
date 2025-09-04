using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entities.UserManagement
{
    [Table("Role", Schema = "account")]
    public class RoleModel:BaseEntity<int>
    {
        public required string Name { get; set; }
        public ICollection<UserRoleModel> UserRoles { get; set; } = new List<UserRoleModel>();
        public ICollection<RoleMenuModel> RoleMenus { get; set; } = new List<RoleMenuModel>();
    }
}

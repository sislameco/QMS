using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Entities;

namespace Models.Entities.UserManagement
{
    [Table("Role", Schema = "UserMgmt")]
    public class RoleModel : BaseEntity<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<UserRoleModel> UserRoles { get; set; } = new List<UserRoleModel>();
        public ICollection<MenuActionRoleMappingModel> MenuActionRoleMappings { get; set; } = new List<MenuActionRoleMappingModel>();
    }
}

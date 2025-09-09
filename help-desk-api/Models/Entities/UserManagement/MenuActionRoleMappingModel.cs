using System.ComponentModel.DataAnnotations.Schema;
using Models.Entities;

namespace Models.Entities.UserManagement
{
    [Table("MenuActionRoleMapping", Schema = "UserMgmt")]
    public class MenuActionRoleMappingModel : BaseEntity<long>
    {
        public int RoleId { get; set; }
        public RoleModel Role { get; set; }
        public long MenuActionMapId { get; set; }
        public MenuActionMapModel MenuActionMap { get; set; }
        public bool IsAllowed { get; set; }
    }
}
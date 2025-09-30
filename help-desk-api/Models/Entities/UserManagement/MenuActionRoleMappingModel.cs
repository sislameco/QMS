using System.ComponentModel.DataAnnotations.Schema;
using Models.Entities;

namespace Models.Entities.UserManagement
{
    [Table("MenuActionRoleMapping", Schema = "UserMgmt")]
    public class MenuActionRoleMappingModel : BaseEntity<int>
    {
        public int FKRoleId { get; set; }
        [ForeignKey("FKRoleId")]
        public RoleModel Role { get; set; }
        public int FKMenuActionMapId { get; set; }
        [ForeignKey("FKMenuActionMapId")]
        public MenuActionMapModel MenuActionMap { get; set; }
        public bool IsAllowed { get; set; }
    }
}
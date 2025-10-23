using System.ComponentModel.DataAnnotations.Schema;
using Models.Entities;
using Models.Entities.Org;

namespace Models.Entities.UserManagement
{
    [Table("MenuActionRoleMapping", Schema = "menu")]
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
    [Table("MenuActionDepartmentMapping", Schema = "menu")]
    public class MenuActionDepartmentMappingModel : BaseEntity<int>
    {
        public int FkDepartmentId { get; set; }
        [ForeignKey("FkDepartmentId")]
        public DepartmentModel Department { get; set; }
        public int FKMenuActionMapId { get; set; }
        [ForeignKey("FKMenuActionMapId")]
        public MenuActionMapModel MenuActionMap { get; set; }
        public bool IsAllowed { get; set; }
    }
}
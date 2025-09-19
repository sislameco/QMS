
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entities.UserManagement
{
    [Table("RoleMenu", Schema = "UserMgmt")]
    public class RoleMenuModel: BaseEntity<int>
    {
        public int RoleId { get; set; }
        public RoleModel Role { get; set; } = default!;

        public int MenuId { get; set; }
        public MenuModel Menu { get; set; } = default!;

        public bool CanView { get; set; } = false;
        public bool CanEdit { get; set; } = false;
        public bool CanDelete { get; set; } = false;
    }

}

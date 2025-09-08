using Models.Entities;
using Models.Entities.UserManagement;

namespace Models.Entities.UserManagement
{
    public class RolePermissionModel : BaseEntity<int>
    {
        public int RoleId { get; set; }
        public RoleModel Role { get; set; } = default!;
        public int PermissionId { get; set; }
        public PermissionModel Permission { get; set; } = default!;
        public int MenuId { get; set; }
        public MenuModel Menu { get; set; } = default!;
    }
}
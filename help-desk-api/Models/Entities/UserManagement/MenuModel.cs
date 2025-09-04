using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entities.UserManagement
{
    public class MenuModel : BaseEntity<int>
    {
        public required string Name { get; set; }
        public string? Url { get; set; }

        public int? ParentId { get; set; }
        public MenuModel? Parent { get; set; }
        public ICollection<MenuModel> Children { get; set; } = new List<MenuModel>();

        public ICollection<RoleMenuModel> RoleMenus { get; set; } = new List<RoleMenuModel>();

    }
}
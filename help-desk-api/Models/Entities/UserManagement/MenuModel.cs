using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Entities;

namespace Models.Entities.UserManagement
{
    [Table("Menu", Schema = "UserMgmt")]
    public class MenuModel : BaseEntity<int>
    {
        public required string Name { get; set; }
        public string? Url { get; set; }

        public int? ParentId { get; set; }
        public MenuModel? Parent { get; set; }
        public ICollection<MenuModel> Children { get; set; } = new List<MenuModel>();

        public ICollection<RoleMenuModel> RoleMenus { get; set; } = new List<RoleMenuModel>();

        public int TemplateId { get; set; }
        public int DisplayOrder { get; set; }
        public string IconClass { get; set; } = null!;
        public string IconViewBox { get; set; } = null!;
        public string Route { get; set; } = null!;
        public ICollection<MenuActionMapModel> MenuActionMaps { get; set; } = null!;
    }
}
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Models.Entities;

namespace Models.Entities.UserManagement
{
    [Table("MenuActionMap", Schema = "UserMgmt")]
    public class MenuActionMapModel : BaseEntity<long>
    {
        public int FKMenuId { get; set; }
        public MenuModel Menu { get; set; }
        public int FKMenuActionId { get; set; }
        public MenuActionModel MenuAction { get; set; }
        public ICollection<MenuActionRoleMappingModel> MenuActionRoleMappings { get; set; }
    }
}
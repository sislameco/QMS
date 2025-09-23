using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Models.Entities;

namespace Models.Entities.UserManagement
{
    [Table("MenuActionMap", Schema = "UserMgmt")]
    public class MenuActionMapModel : BaseEntity<long>
    {
        public int FKMenuId { get; set; }
        [ForeignKey("FKMenuId")]
        public MenuModel Menu { get; set; }
        public string ApiUrl { get; set; }
        public int FKMenuActionId { get; set; }
        [ForeignKey("FKMenuActionId")]
        public MenuActionModel MenuAction { get; set; }
        public ICollection<MenuActionRoleMappingModel> MenuActionRoleMappings { get; set; }
    }
}
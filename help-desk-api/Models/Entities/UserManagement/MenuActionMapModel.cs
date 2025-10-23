using Models.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entities.UserManagement
{
    [Table("MenuActionMap", Schema = "menu")]
    public class MenuActionMapModel : BaseEntity<int>
    {
        public int FKMenuId { get; set; }
        [ForeignKey("FKMenuId")]
        public MenuModel Menu { get; set; }
        [MaxLength(200)]
        public string ApiUrl { get; set; }
        [MaxLength(200)]
        public string RoutePath { get; set; }
        public int FKMenuActionId { get; set; }
        [ForeignKey("FKMenuActionId")]
        public MenuActionModel MenuAction { get; set; }
        public ICollection<MenuActionRoleMappingModel> MenuActionRoleMappings { get; set; }
    }
}
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Models.Entities;

namespace Models.Entities.UserManagement
{
    [Table("MenuAction", Schema = "menu")]
    public class MenuActionModel : BaseEntity<int>
    {
        public string Name { get; set; }
        public string HttpVerb { get; set; }
        public string Description { get; set; }
        public ICollection<MenuActionMapModel> MenuActionMaps { get; set; }
    }
}
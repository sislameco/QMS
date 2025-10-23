using Models.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entities.UserManagement
{
    [Table("AssociateActionRoutes", Schema = "menu")]
    public class AssociateActionRouteModel
    {
        public int? FkMenuActionMapId { get; set; }
        [ForeignKey("FkMenuActionMapId")]
        public MenuActionMapModel MenuActionMap { get; set; }
        [MaxLength(200)]
        public string ApiUrl { get; set; }
        [MaxLength(20)]
        public string HttpVerb { get; set; }
    }
}

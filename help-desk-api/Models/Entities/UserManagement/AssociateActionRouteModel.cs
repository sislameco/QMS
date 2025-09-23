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
    [Table("AssociateActionRoutes")]
    public class AssociateActionRouteModel : BaseEntity<int>
    {
        // Static property to hold current user id (set from HttpContext in your service/repository)
        public static int CurrentUserId { get; set; }
        public long? FkMenuActionMapId { get; set; }
        [ForeignKey("FkMenuActionMapId")]
        public MenuActionMapModel MenuActionMap { get; set; }
        [MaxLength(200)]
        public string ApiUrl { get; set; }
        [MaxLength(20)]
        public string HttpVerb { get; set; }

        public override void OnEntityCreated()
        {
            base.OnEntityCreated();
            CreatedDate = DateTime.UtcNow;
            CreatedBy = CurrentUserId; // Set from static property
        }

        public override void OnEntityUpdated()
        {
            base.OnEntityUpdated();
            UpdatedDate = DateTime.UtcNow;
            UpdatedBy = CurrentUserId;
        }

        public override void OnEntityDeleted()
        {
            base.OnEntityDeleted();
            DeletedDate = DateTime.UtcNow;
            DeletedBy = CurrentUserId;
            RStatus = EnumRStatus.Deleted; // soft delete
        }
    }
}

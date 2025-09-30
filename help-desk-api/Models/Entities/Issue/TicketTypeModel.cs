using Models.Entities.UserManagement;
using Models.Enum;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entities.Issue
{
    [Table("TicketType", Schema = "issue")]
    public class TicketTypeModel : BaseEntity<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsEnabled { get; set; }
        public TicketPriority DefaultPriority { get; set; }
        public int? FKAssignedUserId { get; set; }

        [ForeignKey("FKAssignedUserId")]
        public UserModel User { get; set; }
    }


}
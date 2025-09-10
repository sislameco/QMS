using System.ComponentModel.DataAnnotations.Schema;
using Models.Entities;
using Models.Entities.UserManagement;

namespace Models.Entities.Org
{
    [Table("UserDepartment", Schema = "Org")]
    public class UserDepartmentModel : BaseEntity<long>
    {
        public long FKUserId { get; set; }
        public long FKDepartmentId { get; set; }
        [ForeignKey("FKUserId")]
        public UserModel User { get; set; }
        [ForeignKey("FKDepartmentId")]
        public DepartmentModel Department { get; set; }


    }
}
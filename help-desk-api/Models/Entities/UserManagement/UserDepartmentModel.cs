using Models.Entities;
using Models.Entities.Org;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entities.UserManagement
{
    [Table("UserDepartment", Schema = "UserMgmt")]
    public class UserDepartmentModel : BaseEntity<long>
    {
        public long FKUserId { get; set; }
        public UserModel User { get; set; }
        public long FKDepartmentId { get; set; }
        public DepartmentModel Department { get; set; }
    }
}
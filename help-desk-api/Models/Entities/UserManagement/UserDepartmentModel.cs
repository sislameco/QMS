using Models.Entities.Org;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entities.UserManagement
{
    [Table("UserDepartments", Schema = "UserMgmt")]
    public class UserDepartmentModel : BaseEntity<int>
    {
        public int FkCompanyId { get; set; }
        [ForeignKey("FkCompanyId")]
        public CompanyModel Company { get; set; }
        public int FkUserId { get; set; }
        [ForeignKey("FkUserId")]
        public UserModel Role { get; set; }
        public int FKDepartmentId { get; set; }
        [ForeignKey("FKDepartmentId")]
        public DepartmentModel Department { get; set; }
    }
}
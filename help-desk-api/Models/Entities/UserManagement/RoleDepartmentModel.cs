using Models.Entities;
using Models.Entities.Org;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entities.UserManagement
{
    [Table("RoleDepartment", Schema = "UserMgmt")]
    public class RoleDepartmentModel : BaseEntity<long>
    {
        public long FKRoleId { get; set; }
        public RoleModel Role { get; set; }
        public long FKDepartmentId { get; set; }
        public DepartmentModel Department { get; set; }
    }
}
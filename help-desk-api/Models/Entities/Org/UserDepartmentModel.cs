using System.ComponentModel.DataAnnotations.Schema;
using Models.Entities;

namespace Models.Entities.Org
{
    [Table("UserDepartment", Schema = "Org")]
    public class UserDepartmentModel : BaseEntity<long>
    {
        public long UserId { get; set; }
        public long DepartmentId { get; set; }
        public DepartmentModel Department { get; set; }
    }
}
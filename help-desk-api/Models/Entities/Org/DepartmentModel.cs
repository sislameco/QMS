using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Models.Entities;

namespace Models.Entities.Org
{
    [Table("Department", Schema = "Org")]
    public class DepartmentModel : BaseEntity<long>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public long CompanyId { get; set; }
        public CompanyModel Company { get; set; }
        public ICollection<UserDepartmentModel> UserDepartments { get; set; }
    }
}
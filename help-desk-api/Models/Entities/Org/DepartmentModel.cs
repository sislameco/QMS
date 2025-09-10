using Models.Entities;
using Models.Entities.Issue;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entities.Org
{
    [Table("Department", Schema = "Org")]
    public class DepartmentModel : BaseEntity<long>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public long FKCompanyId { get; set; }
        [ForeignKey("FKCompanyId")]
        public CompanyModel Company { get; set; }
        public ICollection<UserDepartmentModel> UserDepartments { get; set; }
    }
}
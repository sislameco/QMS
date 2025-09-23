using System.ComponentModel.DataAnnotations.Schema;
using Models.Entities.UserManagement;

namespace Models.Entities.Org
{
    [Table("Companies", Schema = "Org")]
    public class CompanyModel : BaseEntity<long>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<CompanyScopeConfigModel> ScopeConfig { get; set; }
        public ICollection<DepartmentModel> Departments { get; set; }
    }

}
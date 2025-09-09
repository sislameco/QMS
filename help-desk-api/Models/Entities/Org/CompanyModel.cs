using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Models.Entities;

namespace Models.Entities.Org
{
    [Table("Company", Schema = "Org")]
    public class CompanyModel : BaseEntity<long>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public CompanyStatus Status { get; set; }
        public CompanyScopeConfigModel ScopeConfig { get; set; }
        public ICollection<DepartmentModel> Departments { get; set; }
        public ICollection<UserCompanyModel> UserCompanies { get; set; }
    }

    public enum CompanyStatus { Active, Inactive }
}
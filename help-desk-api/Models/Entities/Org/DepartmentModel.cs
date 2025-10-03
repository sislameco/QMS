using Models.Entities.UserManagement;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entities.Org
{
    [Table("Department", Schema = "Org")]
    public class DepartmentModel : BaseEntity<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int? IntegrationsPrimaryId { get; set; }
        public int? FKManagerId { get; set; }
        public int? FKCompanyId { get; set; }
        [ForeignKey("FKCompanyId")]
        public CompanyModel Company { get; set; }
        [ForeignKey("FKManagerId")]
        public UserModel User { get; set; }

    }
}
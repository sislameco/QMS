using Models.Entities.Org;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entities.Issue
{
    [Table("CompanyCustomers", Schema = "org")]
    public class CompanyCustomerModel : BaseEntity<int>
    {
        public int FKCompanyId { get; set; }
        [ForeignKey("FKCompanyId")]
        public CompanyModel Company { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string CustomerFirstName { get; set; }
        public string CustomerLastName { get; set; }
    }

    [Table("CompanyProjects", Schema = "org")]
    public class CompanyProjectModel : BaseEntity<int>
    {
        public int FKCompanyId { get; set; }
        [ForeignKey("FKCompanyId")]
        public CompanyModel Company { get; set; }
        public string ProjectName { get; set; }
        public string ReferenceNumber { get; set; }
        public string ProjectAddress { get; set; }
    }
}
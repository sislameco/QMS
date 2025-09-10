using Models.Entities.Org;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entities.Issue
{
    [Table("LeadCustomer", Schema = "issue")]
    public class LeadCustomerModel : BaseEntity<long>
    {
        public long FKCompanyId { get; set; }
        [ForeignKey("FKCompanyId")]
        public CompanyModel Company { get; set; }
        public string ProjectNumber { get; set; }
        public string ProjectAddress { get; set; }
        public string CustomerFirstName { get; set; }
        public string CustomerLastName { get; set; }
    }
}
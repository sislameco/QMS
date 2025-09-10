using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entities.Issue
{
    [Table("LeadCustomer", Schema = "issue")]
    public class LeadCustomerModel : BaseEntity<long>
    {
        public long CompanyId { get; set; }
        public string ProjectNumber { get; set; }
        public string ProjectAddress { get; set; }
        public string CustomerFirstName { get; set; }
        public string CustomerLastName { get; set; }
    }
}
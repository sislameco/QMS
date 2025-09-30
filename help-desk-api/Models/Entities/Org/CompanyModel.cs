using System.ComponentModel.DataAnnotations.Schema;
using Models.Entities.UserManagement;

namespace Models.Entities.Org
{
    [Table("Companies", Schema = "Org")]
    public class CompanyModel : BaseEntity<long>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string AccessKey { get; set; }
        public string SecrateKey { get; set;}
        public string PrefixTicket { get; set; }
        public int LastTicketNumber { get; set; }
        public ICollection<DepartmentModel> Departments { get; set; }
    }

}
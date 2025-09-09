using System.ComponentModel.DataAnnotations.Schema;
using Models.Entities;

namespace Models.Entities.Org
{
    [Table("UserCompany", Schema = "Org")]
    public class UserCompanyModel : BaseEntity<long>
    {
        public long UserId { get; set; }
        public long CompanyId { get; set; }
        public CompanyModel Company { get; set; }
    }
}
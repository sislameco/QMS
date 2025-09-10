using Models.Entities;
using Models.Entities.Org;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entities.UserManagement
{
    [Table("UserCompany", Schema = "UserMgmt")]
    public class UserCompanyModel : BaseEntity<long>
    {
        public long UserId { get; set; }
        public UserModel User { get; set; }
        public long CompanyId { get; set; }
        public CompanyModel Company { get; set; }
    }
}
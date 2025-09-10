using Models.Entities;
using Models.Entities.UserManagement;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entities.Org
{
    [Table("UserCompany", Schema = "Org")]
    public class UserCompanyModel : BaseEntity<long>
    {
        public long FKUserId { get; set; }
        public long FKCompanyId { get; set; }

        [ForeignKey("FKUserId")]
        public UserModel User { get; set; }
        [ForeignKey("FKCompanyId")]
        public CompanyModel Company { get; set; }



    }
}
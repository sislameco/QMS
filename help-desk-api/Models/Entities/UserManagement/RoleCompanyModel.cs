using Models.Entities;
using Models.Entities.Org;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entities.UserManagement
{
    [Table("RoleCompany", Schema = "UserMgmt")]
    public class RoleCompanyModel : BaseEntity<long>
    {
        public long FKRoleId { get; set; }
        public RoleModel Role { get; set; }
        public long FKCompanyId { get; set; }
        public CompanyModel Company { get; set; }
    }
}
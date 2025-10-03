using Models.Entities.Org;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entities.UserManagement
{
    [Table("Users", Schema = "UserMgmt")]
    public class UserModel : BaseEntity<int>
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public DateTime LastLoginDate { get; set; }
        public DateTime LastPasswordChange { get; set; }
        public bool IsReportingManager { get; set; }
        public int? IntegrationsPrimaryId { get; set; }
        public int? FkCompanyId { get; set; }
        [ForeignKey("FkCompanyId")]
        public int? FkDepartmentId { get; set; }
        [ForeignKey("FkDepartmentId")]
        public DepartmentModel Department { get; set; }
        public ICollection<UserRoleModel> UserRoles { get; set; } = new List<UserRoleModel>();
    }
}

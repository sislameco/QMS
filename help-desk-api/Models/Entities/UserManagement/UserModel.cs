using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Models.Entities;

namespace Models.Entities.UserManagement
{
    [Table("User", Schema = "UserMgmt")]
    public class UserModel : BaseEntity<long>
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        public DateTime? LastLoginDate { get; set; }

        public ICollection<UserRoleModel> UserRoles { get; set; } = new List<UserRoleModel>();
        public ICollection<UserCompanyModel> UserCompanies { get; set; } = new List<UserCompanyModel>();
        public ICollection<UserDepartmentModel> UserDepartments { get; set; } = new List<UserDepartmentModel>();
    }
}

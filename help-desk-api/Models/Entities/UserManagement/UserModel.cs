using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Models.Entities;

namespace Models.Entities.UserManagement
{
    [Table("Users", Schema = "UserMgmt")]
    public class UserModel : BaseEntity<long>
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public DateTime LastLoginDate { get; set; }
        public DateTime LastPasswordChange { get; set; }
        public ICollection<UserRoleModel> UserRoles { get; set; } = new List<UserRoleModel>();
        public ICollection<UserDepartmentModel> UserDepartments { get; set; } = new List<UserDepartmentModel>();
    }
}

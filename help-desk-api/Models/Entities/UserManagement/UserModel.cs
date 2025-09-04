using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entities.UserManagement
{
    [Table("Users",Schema = "account")]
    public class UserModel: BaseEntity<int>
    {
        public string Name { get; set; } = string.Empty;
        public required string Email { get; set; }

        public string? PasswordHash { get; set; }

        public string? PasswordSalt { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime? LastLoginAt { get; set; }

        public ICollection<UserRoleModel> UserRoles { get; set; } = new List<UserRoleModel>();
    }
}

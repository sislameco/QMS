using Models.Enum;
using System.ComponentModel.DataAnnotations;

namespace Models.Dto.UserManagement
{
    public class UserInputDto
    {
        [Required]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        public string LastName { get; set; } = string.Empty;
        [Required]
        [Phone]
        public string Phone { get; set; } = string.Empty;
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
        [Required]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }

    public class UserOutPutDto
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string FirstName { get; set; }
        public string Lastname { get; set; }    
        public int RoleId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Roles { get; set; } = string.Empty;
        public EnumRStatus Status { get; set; }
        public string Departments { get; set; } = string.Empty;
    }
}

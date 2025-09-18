using System.ComponentModel.DataAnnotations;

namespace Models.Dto.UserManagement
{
    public class RoleInputDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        public string HomeUrl { get; set; } = string.Empty;
    }
    public class RoleUpdateInputDto : RoleInputDto
    {

        public int Id { get; set; }
    }
       
}
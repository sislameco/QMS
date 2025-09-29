using System.ComponentModel.DataAnnotations;

namespace Models.Dto.UserManagement
{
    public class RoleInputDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;
        [Required]
        public List<RoleSetWithMenuActoinDto> FKMenuActionIds { get; set; } 

    }       
}
using System.ComponentModel.DataAnnotations;

namespace Models.Dto.UserManagement
{
    public class RoleInputDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;
        public List<RoleSetWithMenuActoinDto> FKMenuActionIds { get; set; } 

    }    
    public class RoleDetail
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public List<MenuAccessDto> Menus { get; set; }
    }
}
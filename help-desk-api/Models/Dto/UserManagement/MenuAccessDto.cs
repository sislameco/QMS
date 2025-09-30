namespace Models.Dto.UserManagement
{
    public class ManuActionDto
    {
        public int Id { get; set; }
        public string HttpVerb { get; set; }
    }
    public class ManuWishActionPermissionDto : ManuActionDto
    {
        public bool IsPermitted { get; set; }
        public int FkMenuActionMapId { get; set; }
    }
    public class MenuAccessBasicDto
    {
        public string Menu { get; set; } = string.Empty;
        public int MenuId { get; set; }
        public int? ParentId { get; set; }
    }
    public class MenuAccessDto : MenuAccessBasicDto
    {

        public List<ManuWishActionPermissionDto> Actions { get; set; } = new List<ManuWishActionPermissionDto>();
        public List<MenuAccessDto> Childs { get; set; } = new List<MenuAccessDto>();
    }
    public class RoleSetWithMenuActoinDto
    {
        public bool IsAllowed { get; set; }
        public int FkMenuActionMapId { get; set; }
    }
    public class MenuResourceDto
    {
        public RoleDetail Role { get; set; }
        public List<ManuActionDto> Actions { get; set; } = new List<ManuActionDto>();
        public List<MenuAccessDto> Menus { get; set; } = new List<MenuAccessDto>();
    }
}
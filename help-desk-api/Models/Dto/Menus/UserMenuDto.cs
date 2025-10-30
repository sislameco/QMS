using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dto.Menus
{
    public class PermittedMenuDto
    {
        public string MenuName { get; set; }
        public string SystemMenuName { get; set; }
        public int MenuId { get; set; }
        public string RoutePath { get; set; }
        public int ParentId { get; set; }
        public int FActionId { get; set; }
        public int IsMenuBind { get; set; }
        public string IconClass { get; set; }
        public string IconViewBox { get; set; }
        public int Order { get; set; }
        public int IsHome { get; set; }
    }
    public class UserMenus
    {
        public List<PerMenuDto> PermittedMenus { get; set; }
        public UserInfo UserInfo { get; set; }
        public List<PermittedActionsOutputDto> PermittedActions { get; set; }
    }
    public class UserInfo
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string UserName { get; set; }
        public string ImagePath { get; set; }
        public int RoleId { get; set; }
        public int CompanyId { get; set; }
 
    }
    public class UserBiReportDto
    {
        public bool IsDefault { get; set; }
        public string WorkSpaceId { get; set; }
        public string ReportId { get; set; }
        public string ReportName { get; set; }

    }
    public class UserProfile
    {
        public int ProfileType { get; set; }
        public string ProfilePicture { get; set; }
    }
    public class MenuBasicDto {
        public int Id { get; set; }
        public string MenuName { get; set; }
        public string Icon { get; set; }
        public string Route { get; set; }
        public int DisplayOrder { get; set; }
    }
    public class UserMenuDto: MenuBasicDto
    {
        public int? ParentId { get; set; }
    }

    public class IntegratedMenuOutputDto
    {
        public string MenuName { get; set; }
        public string Icon { get; set; }
        public int DisplayOrder { get; set; }
        public string Route { get; set; }
    }

    public class PerMenuDto: MenuBasicDto
    {
       public List<MenuBasicDto> SubMenus { get; set; }
    }

    public class PermittedActionsOutputDto
    {
        public int UserId { get; set; }
        public int MenuId { get; set; }
        public int MapId { get; set; }
        public string MenuName { get; set; }
        public string HttpVerb { get; set; }
        public string ApiUrl { get; set; }
    }
}

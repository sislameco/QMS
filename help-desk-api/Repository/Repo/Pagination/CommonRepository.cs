using Microsoft.EntityFrameworkCore;
using Models.Dto.Org;
using Models.Dto.Pagination;
using Models.Dto.UserManagement;
using Models.Enum;
using Repository.Db;
using Repository.Seeds;
using System;

namespace Repository.Repo.Pagination
{
    public interface ICommonRepository
    {
        Task<PaginationResponse<RoleWithUsersDto>> GetRolesWithUsersAsync();
        Task<PaginationResponse<DepartmentSettingOutputDto>> GetDepartments(int companyId, DepartmentSettingInputDto input);
    }

    public class CommonRepository : ICommonRepository
    {
        private readonly HelpDbContext _dbContext;

        public CommonRepository(HelpDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<PaginationResponse<RoleWithUsersDto>> GetRolesWithUsersAsync()
        {
            var result = await (
                from role in _dbContext.Roles
                where role.RStatus == EnumRStatus.Active
                select new RoleWithUsersDto
                {
                    RoleId = role.Id,
                    RoleName = role.Name,
                    Type = role.IsSystemGenerated ? "System" : "Custom",
                    Description = role.Description,
                    Users = (
                        from ur in _dbContext.UserRoles
                        join user in _dbContext.Users
                            on ur.FKUserId equals user.Id
                        where ur.FKRoleId == role.Id
                            && ur.RStatus == EnumRStatus.Active
                            && user.RStatus == EnumRStatus.Active
                        select user == null ? "" : string.Concat(user.FirstName, " ", user.LastName)
                    ).FirstOrDefault()
                }
            ).ToListAsync();

            return new PaginationResponse<RoleWithUsersDto>
            {
                Items = result,
                Total = result.Count
            };
        }

        public async Task<PaginationResponse<DepartmentSettingOutputDto>> GetDepartments(int companyId, DepartmentSettingInputDto input)
        {

            input.SearchText = string.IsNullOrWhiteSpace(input.SearchText) ? "" : input.SearchText;
            input.SearchText = input.SearchText?.Trim();
            var data =
                (from dep in _dbContext.Departments
                 join user in _dbContext.Users
                     on dep.Id equals user.FkDepartmentId into users
                 from user in users.DefaultIfEmpty()

                 join man in _dbContext.Users
                     on dep.FKManagerId equals man.Id into managers
                 from man in managers.DefaultIfEmpty()

                 join menud in _dbContext.MenuActionDepartmentMapping
                   on dep.Id equals menud.FkDepartmentId into module
                 from menud in module.DefaultIfEmpty()


                 join menuAction in _dbContext.MenuActionMaps
                 on menud.FKMenuActionMapId equals menuAction.Id into menuActions
                 from menuAction in menuActions.DefaultIfEmpty()


                 join action in _dbContext.MenuActions
                 on menuAction.FKMenuActionId equals action.Id into actions
                 from action in actions.DefaultIfEmpty()

                 join menu in _dbContext.Menus
                 on menuAction.FKMenuId equals menu.Id into menus
                 from menu in menus.DefaultIfEmpty()


                 where dep.FKCompanyId == companyId
                 && dep.RStatus == EnumRStatus.Active
                 && (input.UserIds.Count() == 0 || input.UserIds.Contains(user.Id))
                 && (input.ModuleIds.Count() == 0 || input.ModuleIds.Contains(menu.Id))
                 && (input.SearchText == null || dep.Name.Contains(input.SearchText))

                 select new
                 {
                     Department = dep,
                     Manager = man,
                     Users = user,
                     Menus = menu,
                     TotalUser = user == null ? 0 : 1
                 }).ToList();

            var result =  data
     .GroupBy(x => x.Department.Id)
     .Select(g => new DepartmentSettingOutputDto
     {
         Id = g.Key,
         Name = g.First().Department.Name,
         Description = g.First().Department.Description,
         ManagerEmail = g.First().Manager != null
                        ? g.First().Manager.Email
                        : null,
         ManagerName = g.First().Manager != null
             ? g.First().Manager.FirstName + " " + g.First().Manager.LastName
             : null,
         TotalUsers = g.Sum(s=> s.TotalUser) ,
         Moduls = g
             .Where(s => s.Menus != null && s.Menus != null)
             .Select(s => s.Menus.Name)
             .Distinct()
             .ToArray(),
     })
     .ToList();


            return new PaginationResponse<DepartmentSettingOutputDto>
            {
                Items = result.Skip((input.PageNo - 1) * input.PageNo).Take(input.ItemsPerPage).ToList(),
                Total = result.Count
            };
        }
    }
}

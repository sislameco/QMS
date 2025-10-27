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
            var data =
                from dep in _dbContext.Departments
                join user in _dbContext.Users
                    on dep.Id equals user.FkDepartmentId into users
                join man in _dbContext.Users
                    on dep.FKManagerId equals man.Id into managers
                from man in managers.DefaultIfEmpty()

                join menud in _dbContext.MenuActionDepartmentMapping
                  on dep.Id equals menud.FkDepartmentId into module
                from menud in module.DefaultIfEmpty()


                join menuAction in _dbContext.MenuActionMaps
                on menud.FKMenuActionMapId equals menuAction.Id into menuActions
                from menuAction in menuActions.DefaultIfEmpty()


                join menu in _dbContext.MenuActionMaps
                on menud.FKMenuActionMapId equals menu.Id into menus
                from menu in menus.DefaultIfEmpty()




                where dep.FKCompanyId == companyId && dep.RStatus != EnumRStatus.Deleted
                select new
                {
                    Department = dep,
                    Manager = man,
                    Users = users,
                    Menus = menu
                };

            var result = await data
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
         TotalUsers = g.SelectMany(s => s.Users).Count(),
         Moduls = g
             .Where(s => s.Menus != null && s.Menus.Menu != null)
             .Select(s => s.Menus.Menu.Name)
             .Distinct()
             .ToArray(),
     })
     .ToListAsync();


            return new PaginationResponse<DepartmentSettingOutputDto>
            {
                Items = result,
                Total = result.Count
            };
        }
    }
}

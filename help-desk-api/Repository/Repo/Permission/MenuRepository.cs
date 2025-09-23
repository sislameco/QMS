using Microsoft.EntityFrameworkCore;
using Models.Dto.Dashboard;
using Models.Dto.Menus;
using Repository.Db;
using Repository.Repo.UserManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repo.Permission
{
    public interface IMenuRepository
    {
        Task<List<PermittedActionsOutputDto>> GetPermittedActions(long userId);
    }
    public class MenuRepository : IMenuRepository
    {
        private readonly HelpDbContext _dbContext;

        public MenuRepository(HelpDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<PermittedActionsOutputDto>> GetPermittedActions(long userId)
        {

            // First part: with AssociateActionRoutes
            // Fix for CS1941: The type of one of the expressions in the join clause is incorrect.
            // The join clause: join route in _dbContext.AssociateActionRoutes on map.Id equals route.FkMenuActionMapId
            // Problem: map.Id is long, route.FkMenuActionMapId is int? (nullable int).
            // Solution: Cast map.Id to int and handle nullable route.FkMenuActionMapId.

            var query1 = from menu in _dbContext.Menus
                         join map in _dbContext.MenuActionMaps on menu.Id equals map.FKMenuId into gj1
                         from map in gj1.DefaultIfEmpty()
                         join route in _dbContext.AssociateActionRoutes on (int?)map.Id equals route.FkMenuActionMapId
                         join action in _dbContext.MenuActions on map.FKMenuActionId equals action.Id into gj2
                         from action in gj2.DefaultIfEmpty()
                         join userRole in _dbContext.MenuActionRoleMappings on map.Id equals userRole.FKMenuActionMapId into gj3
                         from userRole in gj3.DefaultIfEmpty()
                         join role in _dbContext.UserRoles on userRole.FKRoleId equals role.Id into gj4
                         from role in gj4.DefaultIfEmpty()
                         join roleMap in _dbContext.UserRoles on role.Id equals roleMap.FKRoleId into gj5
                         from roleMap in gj5.DefaultIfEmpty()
                         where roleMap.FKUserId == userId
                         select new PermittedActionsOutputDto
                         {
                             UserId = userId,
                             MenuId = menu.Id,
                             MapId = map.Id,
                             MenuName = menu.Name,
                             ApiUrl = route.ApiUrl,
                             SystemMenuName = menu.Name,
                             HttpVerb = route.HttpVerb
                         };

            // Second part: directly from MenuActionMap
            var query2 = from menu in _dbContext.Menus
                         join map in _dbContext.MenuActionMaps on menu.Id equals map.FKMenuId into gj1
                         from map in gj1.DefaultIfEmpty()
                         join action in _dbContext.MenuActions on map.FKMenuActionId equals action.Id into gj2
                         from action in gj2.DefaultIfEmpty()
                         join userRole in _dbContext.MenuActionRoleMappings on map.Id equals userRole.FKMenuActionMapId into gj3
                         from userRole in gj3.DefaultIfEmpty()
                         join role in _dbContext.UserRoles on userRole.FKRoleId equals role.Id into gj4
                         from role in gj4.DefaultIfEmpty()
                         join roleMap in _dbContext.UserRoles on role.Id equals roleMap.FKRoleId into gj5
                         from roleMap in gj5.DefaultIfEmpty()
                         where roleMap.FKUserId == userId
                         select new PermittedActionsOutputDto
                         {
                             UserId = userId,
                             MenuId = menu.Id,
                             MapId = map.Id,
                             MenuName = menu.Name,
                             ApiUrl = map.ApiUrl,
                             SystemMenuName = menu.Name,
                             HttpVerb = action.HttpVerb
                         };

            // Combine with Union
            var result = await query1
                .Union(query2)
                .OrderBy(x => x.MenuId)
                .ToListAsync();

            return result;
        }

    }
}

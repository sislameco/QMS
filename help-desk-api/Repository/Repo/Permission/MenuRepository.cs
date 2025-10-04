using Microsoft.EntityFrameworkCore;
using Models.Dto.Menus;
using Models.Dto.UserManagement;
using Repository.Db;
using System;

namespace Repository.Repo.Permission
{
    public interface IMenuRepository
    {
        Task<List<PerMenuDto>> GetUserPermittedMenusAsync(long userId);
        Task<List<MenuAccessDto>> GetRolePermittedMenusAsync(int roleId);
        Task<List<MenuAccessDto>> GetDepartmentPermittedMenusAsync(int departementId);
    }

    public class MenuRepository : IMenuRepository
    {
        private readonly HelpDbContext _dbContext;

        public MenuRepository(HelpDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<PerMenuDto>> GetUserPermittedMenusAsync(long userId)
        {
            var flatMenus = await (
                from m in _dbContext.Menus
                join map in _dbContext.MenuActionMaps on m.Id equals map.FKMenuId into menuActions
                from ma in menuActions.DefaultIfEmpty()
                select new UserMenuDto
                {
                    Id = m.Id,
                    MenuName = m.Name ?? string.Empty,
                    Route = m.Route ?? string.Empty,
                    ParentId = m.ParentId,
                    DisplayOrder = m.DisplayOrder,
                    Icon = m.IconClass ?? string.Empty
                }
            )
            .Distinct()
            .OrderBy(x => x.DisplayOrder)
            .ToListAsync();

            return BuildUserMenuTree(flatMenus);
        }

        public async Task<List<MenuAccessDto>> GetRolePermittedMenusAsync(int roleId)
        {
            var flatList = await (
                from menu in _dbContext.Menus
                join action in _dbContext.MenuActionMaps on menu.Id equals action.FKMenuId into mapAction
                from action in mapAction.DefaultIfEmpty()
                join ac in _dbContext.MenuActions on action.FKMenuActionId equals ac.Id into actionDetails
                from ac in actionDetails.DefaultIfEmpty()
                join map in _dbContext.MenuActionRoleMappings
                    on action.Id equals map.FKMenuActionMapId into actionMapJoin
                from map in actionMapJoin.DefaultIfEmpty()
                where (map != null && map.FKRoleId == roleId) || map == null
                group new { ac, map, action } by new { menu.Id, menu.Name, menu.ParentId } into g
                select new MenuAccessDto
                {
                    MenuId = g.Key.Id,
                    Menu = g.Key.Name,
                    ParentId = g.Key.ParentId,
                    Actions = g
                        .Where(x => x.ac != null)
                        .Select(x => new ManuWishActionPermissionDto
                        {
                            Id = x.ac.Id,
                            HttpVerb = x.ac.HttpVerb ?? string.Empty,
                            IsPermitted = x.map != null && x.map.IsAllowed,
                            FkMenuActionMapId = x.action.Id
                        })
                        .ToList()
                }
            ).ToListAsync();

            return BuildRoleMenuTree(flatList);
        }

        #region Helpers

        private static List<PerMenuDto> BuildUserMenuTree(List<UserMenuDto> flatMenus)
        {
            return flatMenus
                .Where(m => m.ParentId == null) // Top-level menus
                .OrderBy(m => m.DisplayOrder)
                .Select(m => new PerMenuDto
                {
                    Id = m.Id,
                    MenuName = m.MenuName,
                    Route = m.Route,
                    DisplayOrder = m.DisplayOrder,
                    Icon = m.Icon,
                    SubMenus = flatMenus
                        .Where(sm => sm.ParentId == m.Id)
                        .OrderBy(sm => sm.DisplayOrder)
                        .Select(sm => new MenuBasicDto
                        {
                            Id = sm.Id,
                            MenuName = sm.MenuName,
                            Route = sm.Route,
                            DisplayOrder = sm.DisplayOrder,
                            Icon = sm.Icon
                        })
                        .ToList()
                })
                .ToList();
        }

        private static List<MenuAccessDto> BuildRoleMenuTree(List<MenuAccessDto> flatList)
        {
            var parents = flatList.Where(s => s.ParentId == null).ToList();

            foreach (var parent in parents)
            {
                parent.Childs = flatList
                    .Where(s => s.ParentId == parent.MenuId)
                    .Select(s => new MenuAccessDto
                    {
                        Actions = s.Actions.DistinctBy(x => x.Id).ToList(),
                        Menu = s.Menu,
                        MenuId = s.MenuId,
                        ParentId = s.ParentId
                    })
                    .ToList();
            }

            return parents;
        }

        #endregion



        public async Task<List<MenuAccessDto>> GetDepartmentPermittedMenusAsync(int departementId)
        {
            var flatList = await (
                from menu in _dbContext.Menus
                join action in _dbContext.MenuActionMaps on menu.Id equals action.FKMenuId into mapAction
                from action in mapAction.DefaultIfEmpty()
                join ac in _dbContext.MenuActions on action.FKMenuActionId equals ac.Id into actionDetails
                from ac in actionDetails.DefaultIfEmpty()
                join map in _dbContext.MenuActionDepartmentMapping
                    on action.Id equals map.FKMenuActionMapId into actionMapJoin
                from map in actionMapJoin.DefaultIfEmpty()
                where (map != null && map.FkDepartmentId == departementId) || map == null
                group new { ac, map, action } by new { menu.Id, menu.Name, menu.ParentId } into g
                select new MenuAccessDto
                {
                    MenuId = g.Key.Id,
                    Menu = g.Key.Name,
                    ParentId = g.Key.ParentId,
                    Actions = g
                        .Where(x => x.ac != null)
                        .Select(x => new ManuWishActionPermissionDto
                        {
                            Id = x.ac.Id,
                            HttpVerb = x.ac.HttpVerb ?? string.Empty,
                            IsPermitted = x.map != null && x.map.IsAllowed,
                            FkMenuActionMapId = x.action.Id
                        })
                        .ToList()
                }
            ).ToListAsync();

            return BuildRoleMenuTree(flatList);
        }
    }
}

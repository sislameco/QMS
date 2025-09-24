using Microsoft.EntityFrameworkCore;
using Models.Dto.Menus;
using Repository.Db;


namespace Repository.Repo.Permission
{
    public interface IMenuRepository
    {
        Task<List<PerMenuDto>> GetPermittedActions(long userId);
    }

    public class MenuRepository : IMenuRepository
    {
        private readonly HelpDbContext _dbContext;

        public MenuRepository(HelpDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<PerMenuDto>> GetPermittedActions(long userId)
        {
            var flatMenus =
                (from m in _dbContext.Menus
                 where m.ParentId == null
                 select new UserMenuDto
                 {
                     Id = m.Id,
                     MenuName = m.Name ?? "",
                     Route = m.Route,
                     ParentId = m.ParentId,
                     DisplayOrder = m.DisplayOrder,
                     Icon = m.IconClass
                 })
                .Union
                (from m in _dbContext.Menus
                 join map in _dbContext.MenuActionMaps on m.Id equals map.FKMenuId
                 select new UserMenuDto
                 {
                     Id = m.Id,
                     MenuName = m.Name,
                     Route = m.Route,
                     ParentId = m.ParentId,
                     DisplayOrder = m.DisplayOrder,
                     Icon = m.IconClass
                 })
                .Distinct()
                .OrderBy(x => x.DisplayOrder)
                .ToList();

            var menuTree = flatMenus
    .Where(m => m.ParentId == null) // top-level menus
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
            .ToList().Select(sm => new MenuBasicDto
            {
                Id = sm.Id,
                MenuName = sm.MenuName,
                Route = sm.Route,
                DisplayOrder = sm.DisplayOrder,
                Icon = sm.Icon
            }).ToList()
    })
    .OrderBy(m => m.DisplayOrder)
    .ToList();
            return menuTree;

        }
    }
}

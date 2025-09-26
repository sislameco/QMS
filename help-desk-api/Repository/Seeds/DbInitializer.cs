using Microsoft.EntityFrameworkCore;
using Models.Entities.UserManagement;
using Repository.Db;

namespace Repository.Seeds
{
    public static class DbInitializer
    {
        public static async Task SeedDailyAvailabilityAsync(HelpDbContext context)
        {
            if (!await context.Users.AnyAsync())
            {
                await context.Users.AddRangeAsync(SeedData.Users);
                await context.SaveChangesAsync();
            }
            if(!await context.Companies.AnyAsync())
            {
                await context.Companies.AddRangeAsync(CompanySeedData.companies);
                await context.SaveChangesAsync();
            }
            if (!await context.Menus.AnyAsync())
            {
                await context.Menus.AddRangeAsync(MenuSeedData.menus);
                await context.SaveChangesAsync();
            }
            if (!await context.Roles.AnyAsync())
            {
                await context.Roles.AddRangeAsync(RoleSeedData.menus);
                await context.SaveChangesAsync();
            }
            if (!await context.MenuActions.AnyAsync())
            {
                await context.MenuActions.AddRangeAsync(MenuActionSeedData.menuActions);
                await context.SaveChangesAsync();
            }
            if (!await context.MenuActionMaps.AnyAsync())
            {
                await context.MenuActionMaps.AddRangeAsync(MenuActionMapModelSeedData.menuActionMaps);
                await context.SaveChangesAsync();
            }
        }
    }
}

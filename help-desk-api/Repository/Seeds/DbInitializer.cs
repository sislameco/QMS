using Microsoft.EntityFrameworkCore;
using Models.Entities.UserManagement;
using Repository.Db;

namespace Repository.Seeds
{
    public static class DbInitializer
    {
        public static async Task SeedDailyAvailabilityAsync(HelpDbContext context)
        {
            if (!await context.Menus.AnyAsync())
            {
                await context.Menus.AddRangeAsync(DefaultMenus);
                //await context.SaveChangesAsync();
            }
        }
    }
}

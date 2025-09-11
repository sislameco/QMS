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
                //await context.Menus.AddRangeAsync(DefaultMenus);
                //await context.SaveChangesAsync();
            }
        }
//        private static readonly List<MenuModel> DefaultMenus =
//[
//    new() { Id = 1, Name = "Dashboard", Url = "/dashboard", ParentId = null },
//    new() { Id = 2, Name = "User Management", Url = null, ParentId = null },
//    new() { Id = 3, Name = "Users", Url = "/users", ParentId = 2 },
//    new() { Id = 4, Name = "Roles", Url = "/roles", ParentId = 2 },
//    new() { Id = 5, Name = "Settings", Url = null, ParentId = null },
//    new() { Id = 6, Name = "System Logs", Url = "/logs", ParentId = 5 }
//];
    }
}

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
                await context.Users.AddRangeAsync(UserSeedData.Users);
                await context.SaveChangesAsync();
            }
            if(!await context.Companies.AnyAsync())
            {
                await context.Companies.AddRangeAsync(CompanySeedData.companies);
                await context.SaveChangesAsync();
            }
        }
    }
}

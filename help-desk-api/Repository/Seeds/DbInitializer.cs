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
            if (!await context.Roles.AnyAsync())
            {
                await context.Roles.AddRangeAsync(RoleSeedData.menus);
                await context.SaveChangesAsync();
            }
            if (!await context.UserRoles.AnyAsync())
            {
                await context.UserRoles.AddRangeAsync(UserRoleSeedData.userRoles);
                await context.SaveChangesAsync();
            }
            if (!await context.Companies.AnyAsync())
            {
                await context.Companies.AddRangeAsync(CompanySeedData.companies);
                await context.SaveChangesAsync();
            }
            if (!await context.Menus.AnyAsync())
            {
                await context.Menus.AddRangeAsync(MenuSeedData.menus);
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
            if (!await context.AssociateActionRoutes.AnyAsync())
            {
                await context.AssociateActionRoutes.AddRangeAsync(AssociateActionRouteSeedData.associateActionRoutes);
                await context.SaveChangesAsync();
            }
            if (!await context.CompanyDefineDataSources.AnyAsync())
            {
                await context.CompanyDefineDataSources.AddRangeAsync(CompanyDefineDataSourceSeedData.companyDefineDataSources);
                await context.SaveChangesAsync();
            }
            if (!await context.EmailConfigurations.AnyAsync())
            {
                await context.EmailConfigurations.AddRangeAsync(EmailConfigurationSeedData.emailConfigurations);
                await context.SaveChangesAsync();
            }
            if (!await context.NotificationTemplates.AnyAsync())
            {
                await context.NotificationTemplates.AddRangeAsync(NotificationTemplateModelSeedData.notifications);
                await context.SaveChangesAsync();
            }
        }
    }
}

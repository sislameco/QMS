using Microsoft.EntityFrameworkCore;
using Models.Entities.UserManagement;
using Models.Entities.Audit;
using Models.Entities.Org;
using Models.Entities.Setup;
using Models.Entities.Notification;
using Models.Entities.Issue;

namespace Repository.Db
{
    public class HelpDbContext : DbContext
    {
        public HelpDbContext(DbContextOptions<HelpDbContext> options) : base(options) { }

        public DbSet<UserModel> Users => Set<UserModel>();
        public DbSet<RoleModel> Roles => Set<RoleModel>();
        public DbSet<UserRoleModel> UserRoles => Set<UserRoleModel>();

        public DbSet<UserLoginModel> UserLogins => Set<UserLoginModel>();
        public DbSet<RefreshTokenModel> RefreshTokens => Set<RefreshTokenModel>();

        public DbSet<MenuModel> Menus => Set<MenuModel>();

        public DbSet<MenuActionModel> MenuActions => Set<MenuActionModel>();
        public DbSet<MenuActionMapModel> MenuActionMaps => Set<MenuActionMapModel>();
        public DbSet<MenuActionRoleMappingModel> MenuActionRoleMappings => Set<MenuActionRoleMappingModel>();
        public DbSet<MenuActionDepartmentMappingModel> MenuActionDepartmentMapping => Set<MenuActionDepartmentMappingModel>();
        
        public DbSet<AssociateActionRouteModel> AssociateActionRoutes => Set<AssociateActionRouteModel>();
        public DbSet<AuditLogModel> AuditLogs => Set<AuditLogModel>();
        public DbSet<CompanyModel> Companies => Set<CompanyModel>();
        public DbSet<CompanyDefineDataSourceModel> CompanyDefineDataSources => Set<CompanyDefineDataSourceModel>();
        public DbSet<SLAConfigurationModel> SLAs => Set<SLAConfigurationModel>();
        public DbSet<CustomFieldModel> CustomFields { get; set; }
        public DbSet<TicketCustomFieldValue> TicketCustomFields { get; set; }
        public DbSet<DepartmentModel> Departments => Set<DepartmentModel>();
        public DbSet<EmailConfigurationModel> EmailConfigurations => Set<EmailConfigurationModel>();
        public DbSet<NotificationScheduleModel> NotificationSchedules => Set<NotificationScheduleModel>();
        public DbSet<NotificationTemplateModel> NotificationTemplates => Set<NotificationTemplateModel>();
        public DbSet<ResolutionModel> Resolutions => Set<ResolutionModel>();
        public DbSet<RootCauseModel> RootCauses => Set<RootCauseModel>();
        public DbSet<TicketAttachmentModel> TicketAttachments => Set<TicketAttachmentModel>();
        public DbSet<TicketCommentModel> TicketComments => Set<TicketCommentModel>();
        public DbSet<TicketDepartmentMapModel> TicketDepartmentMaps => Set<TicketDepartmentMapModel>();
        public DbSet<TicketLeadCustomerMapModel> TicketLeadCustomerMaps => Set<TicketLeadCustomerMapModel>();
        public DbSet<TicketLinkModel> TicketLinks => Set<TicketLinkModel>();
        public DbSet<TicketModel> Tickets => Set<TicketModel>();
        public DbSet<TicketTypeModel> TicketTypes => Set<TicketTypeModel>();
        public DbSet<TicketWatchListModel> TicketWatchLists => Set<TicketWatchListModel>();
        public DbSet<LeadCustomerModel> LeadCustomers => Set<LeadCustomerModel>();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}

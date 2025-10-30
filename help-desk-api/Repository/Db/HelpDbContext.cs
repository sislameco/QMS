using Microsoft.EntityFrameworkCore;
using Models.Entities.Audit;
using Models.Entities.Auth;
using Models.Entities.File;
using Models.Entities.Issue;
using Models.Entities.Notification;
using Models.Entities.Org;
using Models.Entities.Setup;
using Models.Entities.UserManagement;

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
        public DbSet<RecoverPasswordTokenModel> RecoverPasswordTokens => Set<RecoverPasswordTokenModel>();
        public DbSet<MenuModel> Menus => Set<MenuModel>();
        public DbSet<MenuActionModel> MenuActions => Set<MenuActionModel>();
        public DbSet<MenuActionMapModel> MenuActionMaps => Set<MenuActionMapModel>();
        public DbSet<MenuActionRoleMappingModel> MenuActionRoleMappings => Set<MenuActionRoleMappingModel>();
        public DbSet<MenuActionDepartmentMappingModel> MenuActionDepartmentMapping => Set<MenuActionDepartmentMappingModel>();       
        public DbSet<AssociateActionRouteModel> AssociateActionRoutes => Set<AssociateActionRouteModel>();
        public DbSet<AuditLogModel> AuditLogs => Set<AuditLogModel>();
        public DbSet<QMSAuditLogModel> QMSAuditLog => Set<QMSAuditLogModel>();
        public DbSet<CompanyModel> Companies => Set<CompanyModel>();
        public DbSet<CompanyDefineDataSourceModel> CompanyDefineDataSources => Set<CompanyDefineDataSourceModel>();
        public DbSet<SLAConfigurationModel> SLAs => Set<SLAConfigurationModel>();
        public DbSet<TempFileModel> TempFiles => Set<TempFileModel>();
        public DbSet<CustomFieldModel> CustomFields { get; set; }
        public DbSet<TicketCustomFieldValueModel> TicketCustomFields { get; set; }
        public DbSet<DepartmentModel> Departments => Set<DepartmentModel>();
        public DbSet<EmailConfigurationModel> EmailConfigurations => Set<EmailConfigurationModel>();
        public DbSet<NotificationScheduleModel> NotificationSchedules => Set<NotificationScheduleModel>();
        public DbSet<NotificationTemplateModel> NotificationTemplates => Set<NotificationTemplateModel>();
        public DbSet<CompanyDefineRootResolutionModel> CompanyDefineRootResolutions => Set<CompanyDefineRootResolutionModel>();
        public DbSet<TicketAttachmentModel> TicketAttachments => Set<TicketAttachmentModel>();
        public DbSet<TicketCommentModel> TicketComments => Set<TicketCommentModel>();
        public DbSet<TicketDepartmentMapModel> TicketDepartmentMaps => Set<TicketDepartmentMapModel>();
        public DbSet<TicketCustomerMapModel> TicketLeadCustomerMaps => Set<TicketCustomerMapModel>();
        public DbSet<TicketProjectMapModel> TicketProjectMaps => Set<TicketProjectMapModel>();
        public DbSet<TicketLinkModel> TicketLinks => Set<TicketLinkModel>();
        public DbSet<TicketModel> Tickets => Set<TicketModel>();
        public DbSet<TicketTypeModel> TicketTypes => Set<TicketTypeModel>();
        public DbSet<TicketWatchListModel> TicketWatchLists => Set<TicketWatchListModel>();
        public DbSet<CompanyCustomerModel> CompanyCustomers => Set<CompanyCustomerModel>();
        public DbSet<CompanyProjectModel> CompanyProjects => Set<CompanyProjectModel>();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}

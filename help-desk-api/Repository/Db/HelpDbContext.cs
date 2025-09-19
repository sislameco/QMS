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
        public DbSet<UserLoginModel> UserLogins => Set<UserLoginModel>();
        public DbSet<RoleModel> Roles => Set<RoleModel>();
        public DbSet<MenuModel> Menus => Set<MenuModel>();
        public DbSet<UserRoleModel> UserRoles => Set<UserRoleModel>();
        public DbSet<MenuActionModel> MenuActions => Set<MenuActionModel>();
        public DbSet<MenuActionMapModel> MenuActionMaps => Set<MenuActionMapModel>();
        public DbSet<MenuActionRoleMappingModel> MenuActionRoleMappings => Set<MenuActionRoleMappingModel>();
        public DbSet<RoleMenuModel> RoleMenus => Set<RoleMenuModel>();
        public DbSet<RoleCompanyModel> RoleCompanies => Set<RoleCompanyModel>();
        public DbSet<RoleDepartmentModel> RoleDepartments => Set<RoleDepartmentModel>();
        public DbSet<AuditLogModel> AuditLogs => Set<AuditLogModel>();
        public DbSet<CompanyModel> Companies => Set<CompanyModel>();
        public DbSet<CompanyScopeConfigModel> CompanyScopeConfigs => Set<CompanyScopeConfigModel>();
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

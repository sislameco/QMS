using Microsoft.EntityFrameworkCore;
using Models.Entities.UserManagement;



namespace Repository.Db
{
    public class HelpDbContext : DbContext
    {
        public HelpDbContext(DbContextOptions<HelpDbContext> options) : base(options) { }
        public DbSet<UserModel> Users => Set<UserModel>();
        public DbSet<RoleModel> Roles => Set<RoleModel>();
        public DbSet<MenuModel> Menus => Set<MenuModel>();
        public DbSet<UserRoleModel> UserRoles => Set<UserRoleModel>();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}

using Microsoft.EntityFrameworkCore;



namespace Repository.Db
{
    public class HelpDbContext : DbContext
    {
        public HelpDbContext(DbContextOptions<HelpDbContext> options) : base(options) { }
        //public DbSet<UserModel> Users => Set<UserModel>();
        //public DbSet<RoleModel> Roles => Set<RoleModel>();
        //public DbSet<MenuModel> Menus => Set<MenuModel>();
        //public DbSet<UserRoleModel> UserRoles => Set<UserRoleModel>();
        //public DbSet<RoleMenuModel> RoleMenus => Set<RoleMenuModel>();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<MenuModel>()
            //    .HasOne(m => m.Parent)
            //    .WithMany(p => p.Children)
            //    .HasForeignKey(m => m.ParentId)
            //    .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

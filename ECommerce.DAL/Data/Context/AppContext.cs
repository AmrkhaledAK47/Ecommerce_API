using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.DAL
{
    public class AppContext : IdentityDbContext<AppUser, AppRole, string>
    {
        public AppContext() : base()
        {
        }
        public AppContext(DbContextOptions<AppContext> options) : base(options)
        {
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    string connectionString = "Server=.\\SQLEXPRESS;DataBase=Lab03MVC;Trusted_Connection=true;TrustServerCertificate=true";
        //    optionsBuilder.
        //        UseSqlServer(connectionString);
        //}
        public override int SaveChanges()
        {
            AuditLog();
            return base.SaveChanges();
        }
        public void AuditLog()
        {
            var DateTimeNow = DateTime.UtcNow;
            foreach (var entry in ChangeTracker.Entries<IAuditableEntity>())
            {
                var entity = entry.Entity;
                if (entry.State == EntityState.Added)
                {
                    entity.CreatedAt = DateTimeNow;
                }
                else if (entry.State == EntityState.Modified)
                {
                    entity.UpdatedAt = DateTimeNow;
                }
            }
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppContext).Assembly);
        }
        public virtual DbSet<Product> Products => Set<Product>();
        public virtual DbSet<Category> Categories => Set<Category>();
    }
}

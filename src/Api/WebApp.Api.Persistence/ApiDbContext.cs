using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApp.Api.Domain.Audit;
using WebApp.Api.Domain.Entities;

namespace WebApp.Api.Persistence
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
        {

        }
        
        public DbSet<Post> Posts { get; set; }
        public DbSet<PostCategory> PostCategories { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectAccount> ProjectAccounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApiDbContext).Assembly);
            
            base.OnModelCreating(modelBuilder);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
        {
            foreach (var entry in ChangeTracker.Entries<IAuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedAtUtc = DateTime.UtcNow;
                        entry.Entity.CreatedBy = "";
                        break;
                    case EntityState.Modified:
                        entry.Entity.UpdatedAtUtc = DateTime.UtcNow;
                        entry.Entity.LastModifiedBy = "";
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}

using LogInSignUp.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogInSignUp.DataAccess.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {}

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity => 
            {
                entity.HasIndex(x => x.UserName).IsUnique();
                entity.HasIndex(x => x.Email).IsUnique();
            });
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entities = ChangeTracker.Entries<BaseEntity>();
            foreach (var entity in entities)
            {
                switch (entity.State)
                {
                    case EntityState.Added:
                        entity.Entity.CreatedAt = DateTime.UtcNow;
                        entity.Entity.IsActive = true;
                        break;
                    case EntityState.Modified:
                        entity.Entity.UpdatedAt = DateTime.UtcNow;
                        break;
                }
            }

            var users = ChangeTracker.Entries<User>();
            foreach (var user in users)
            {
                switch (user.State)
                {
                    case EntityState.Added:
                        user.Entity.IsEmailVerified = false;
                        break;
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}

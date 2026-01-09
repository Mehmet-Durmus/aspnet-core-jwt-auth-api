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

            modelBuilder.Entity<User>(user => 
            {
                user.Property(u => u.Name).HasMaxLength(50);
                user.Property(u => u.LastName).HasMaxLength(50);
                user.Property(u => u.UserName).HasMaxLength(50);
                user.Property(u => u.Email).HasMaxLength(50);
                user.Property(u => u.IsEmailVerified).HasDefaultValue(false);
                user.Property(u => u.EmailVerificationTokenHash).HasColumnType("varbinary(32)");
                user.Property(u => u.PasswordHash).HasMaxLength(255);
                user.Property(u => u.PasswordResetTokenHash).HasColumnType("varbinary(32)");
                user.Property(u => u.RefreshTokenHash).HasColumnType("varbinary(32)");
                user.Property(u => u.IsActive).HasDefaultValue(true);
                user.HasIndex(u => u.UserName).IsUnique();
                user.HasIndex(u => u.Email).IsUnique();
                user.HasQueryFilter(u => u.IsActive);
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

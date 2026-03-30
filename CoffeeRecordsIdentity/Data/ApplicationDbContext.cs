using CoffeeRecordsIdentity.Areas.Identity.Data;
using CoffeeRecordsIdentity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace CoffeeRecordsIdentity.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<CoffeeCup> Cups { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.ConfigureWarnings(warnings => warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var hasher = new PasswordHasher<ApplicationUser>();
            var role = new IdentityRole { Id = "admin", Name = "Admin", NormalizedName = "ADMIN" };
            var user = new ApplicationUser {
                Id = "admin-user-id",
                UserName = "Administrator",
                Email = "admin@admin.com",
                NormalizedEmail = "ADMIN@ADMIN.COM",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            user.PasswordHash = hasher.HashPassword(user, "heslo");

            builder.Entity<IdentityRole>().HasData(role);
            builder.Entity<ApplicationUser>().HasData(user);
            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string> { 
                UserId = user.Id, 
                RoleId = role.Id
            });

        }
    }
}

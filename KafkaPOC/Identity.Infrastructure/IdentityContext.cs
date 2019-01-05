using Identity.Models;
using Microsoft.EntityFrameworkCore;

namespace Identity.Infrastructure
{
    public class IdentityContext : DbContext
    {
        public IdentityContext(DbContextOptions<IdentityContext> options) : base(options)
        {

        }

        public DbSet<ApplicationUserClaim> ApplicationUserClaims { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ApplicationUser>()
                 .HasIndex(u => u.Id)
                 .IsUnique();
            builder.Entity<ApplicationUser>()
                 .HasIndex(u => u.UserName)
                 .IsUnique();
        }

    }
}

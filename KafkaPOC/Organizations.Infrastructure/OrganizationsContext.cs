using Microsoft.EntityFrameworkCore;

namespace Organizations.Infrastructure
{
    public class OrganizationsContext : DbContext
    {
        public OrganizationsContext(DbContextOptions<OrganizationsContext> options) : base(options)
        {

        }

        


        protected override void OnModelCreating(ModelBuilder builder)
        {
            //builder.Entity<ApplicationUser>()
            //     .HasIndex(u => u.Id)
            //     .IsUnique();
        
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Projections.Domain.Models;

namespace Projections.Infrastructure
{
    public class ProjectionsContext : DbContext
    {
        public ProjectionsContext(DbContextOptions<ProjectionsContext> options) : base(options)
        {

        }

        public DbSet<Usage> Usages { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Usage>()
                 .HasIndex(u => u.ID)
                 .IsUnique();
            builder.Entity<Usage>()
                 .HasIndex(u => u.UserId);
            builder.Entity<Usage>()
                 .HasIndex(u => u.OrgId);
            builder.Entity<Usage>()
                 .HasIndex(u => u.OrgName);
            builder.Entity<Usage>()
                 .HasIndex(u => u.FullName);
        }
    }
}

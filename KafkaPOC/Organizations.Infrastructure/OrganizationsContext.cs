using Microsoft.EntityFrameworkCore;
using Organizations.Domain.Models;

namespace Organizations.Infrastructure
{
    public class OrganizationsContext : DbContext
    {
        public OrganizationsContext(DbContextOptions<OrganizationsContext> options) : base(options)
        {
     
        }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<AddressType> AddressTypes { get; set; }
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<OrganizationContact> OrganizationContacts { get; set; }
        public DbSet<OrgType> OrgTypes { get; set; }
        public DbSet<StateProvince> StateProvinces { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Address>()
                 .HasIndex(u => u.ID)
                 .IsUnique();
            builder.Entity<Address>()
                 .HasIndex(u => u.PostalCode);
            builder.Entity<Address>()
                 .HasIndex(u => u.TypeOfAddress);

            builder.Entity<Organization>()
                 .HasIndex(u => u.ID)
                 .IsUnique();
            builder.Entity<Organization>()
                 .HasIndex(u => u.Name);
            builder.Entity<Organization>()
                 .HasIndex(u => u.ParentId);

            builder.Entity<OrganizationContact>()
                 .HasIndex(u => u.ID)
                 .IsUnique();

            builder.Entity<OrgType>()
                 .HasIndex(u => u.ID)
                 .IsUnique();

            builder.Entity<StateProvince>()
                 .HasIndex(u => u.ID)
                 .IsUnique();
            builder.Entity<StateProvince>()
                 .HasIndex(u => u.Name);

            builder.Entity<User>()
                 .HasIndex(u => u.ID)
                 .IsUnique();
            builder.Entity<User>()
                 .HasIndex(u => u.FirstName);
            builder.Entity<User>()
                 .HasIndex(u => u.LastName);
            builder.Entity<User>()
                 .HasIndex(u => u.Email);

        }
    }
}

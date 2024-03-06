using EBTCO.Domain;
using EBTCO.Domain.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EBTCO.DB
{
    public class AppDbContext : IdentityDbContext<User, Role, Guid>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Employee>().OwnsOne(e => e.Name);
            builder.Entity<Owner>().OwnsOne(e => e.Name);
        }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<PropOwner> PropOwners { get; set; }
        public DbSet<SalesOffice> SalesOffices { get; set; }
    }
}

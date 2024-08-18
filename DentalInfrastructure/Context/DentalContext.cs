using DentalDomain.Clinics;
using DentalDomain.Services;
using DentalDomain.Users.Clients;
using DentalDomain.Users.Staffs;
using DentalDomain.Users.SuperAdmin;
using Microsoft.EntityFrameworkCore;

namespace DentalInfrastructure.Context
{
    public class DentalContext : DbContext
    {
        public DentalContext(DbContextOptions<DentalContext> opt) : base(opt) { }

        public DbSet<Staff> Staffs { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<SuperAdmin> SuperAdmin { get; set; }
        public DbSet<Clinic> Clinics { get; set; }
        public DbSet<Service> Services { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DentalContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}

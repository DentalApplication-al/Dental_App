using DentalDomain.Users.SuperAdmin;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DentalInfrastructure.Configuration
{
    public class SuperAdminConfiguration : IEntityTypeConfiguration<SuperAdmin>
    {
        public void Configure(EntityTypeBuilder<SuperAdmin> builder)
        {
            builder.HasData(
                new SuperAdmin
                {
                    Username = "SuperAdmin",
                    Password = "password",
                });
        }
    }
}

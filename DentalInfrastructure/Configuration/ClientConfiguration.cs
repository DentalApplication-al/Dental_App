using DentalDomain.Users.Clients;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DentalInfrastructure.Configuration
{
    public class ClientConfiguration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.HasMany(a => a.Appointments)
                .WithOne(a => a.Client)
                .HasForeignKey(a => a.ClientId);
        }
    }
}

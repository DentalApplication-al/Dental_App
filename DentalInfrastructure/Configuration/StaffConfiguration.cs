using DentalDomain.Users.Staffs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DentalInfrastructure.Configuration
{
    public class StaffConfiguration : IEntityTypeConfiguration<Staff>
    {
        public void Configure(EntityTypeBuilder<Staff> builder)
        {
            builder
                .HasMany(a => a.Appointments)
                .WithMany(a => a.Doctor);
        }
    }
}

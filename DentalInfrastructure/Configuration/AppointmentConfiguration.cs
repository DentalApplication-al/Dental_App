using DentalDomain.Appointments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DentalInfrastructure.Configuration
{
    public class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> builder)
        {
            builder
                .HasOne(a => a.Client)
                .WithMany(a => a.Appointments)
                .HasForeignKey(a => a.ClientId);

            builder
                .HasOne(a => a.Service)
                .WithMany()
                .HasForeignKey(a => a.ServiceId);

            builder
                .HasOne(a => a.Doctor)
                .WithMany(a => a.Appointments)
                .HasForeignKey(a => a.DoctorId);

            builder.HasMany(a => a.Files)
                .WithOne(a => a.Appointment)
                .HasForeignKey(a => a.AppointmentId);
        }
    }
}

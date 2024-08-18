namespace DentalInfrastructure.Configuration
{
    //public class StaffConfiguration : IEntityTypeConfiguration<Staff>
    //{
    //    public void Configure(EntityTypeBuilder<Staff> builder)
    //    {
    //        builder
    //        .HasMany(s => s.StaffServices)
    //        .WithMany(s => s.ServiceStaff)
    //        .UsingEntity<Dictionary<string, object>>(
    //            "StaffServices", // Name of the join table
    //            j => j
    //                .HasOne<Service>()
    //                .WithMany()
    //                .HasForeignKey("ServiceId")
    //                .OnDelete(DeleteBehavior.Cascade), // Optional: handle delete behavior
    //            j => j
    //                .HasOne<Staff>()
    //                .WithMany()
    //                .HasForeignKey("StaffId")
    //                .OnDelete(DeleteBehavior.Cascade), // Optional: handle delete behavior
    //            j =>
    //            {
    //                j.HasKey("StaffId", "ServiceId"); // Composite key for the join table
    //            });

    //    }
    //}
}

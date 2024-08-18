namespace DentalInfrastructure.Configuration
{
    //public class StaffServiceConfiguration : IEntityTypeConfiguration<StaffServices>
    //{
    //    public void Configure(EntityTypeBuilder<StaffServices> builder)
    //    {
    //        builder.HasKey(x => new { x.StaffId, x.ServiceId});
    //        builder
    //            .HasOne(a => a.Staff)
    //            .WithMany(a => a.Services)
    //            .HasForeignKey(a => a.StaffId);

    //        builder
    //            .HasOne(a => a.Service)
    //            .WithMany(a => a.ServiceStaff)
    //            .HasForeignKey(a => a.ServiceId);
    //        builder.ToTable("StaffServices");
    //    }
    //}
}

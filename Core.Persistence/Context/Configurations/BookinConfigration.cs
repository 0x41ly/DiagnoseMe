using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Persistence.Context.Configurations;

public class BookingsConfigration : BaseConfiguration<Booking>
{
    public override void Configure(EntityTypeBuilder<Booking> builder=null!)
    {
        builder.ToTable("Bookings");
        base.Configure(builder);
        builder.Property(c => c.DoctorId).IsRequired();
        builder.Property(mr => mr.PatientId).IsRequired();
        builder.HasOne(p => p.Doctor).WithMany(ps => ps.Bookings).HasForeignKey(ps =>ps.Id);
        builder.HasOne(p => p.Patient).WithMany(ps => ps.Bookings).HasForeignKey(ps =>ps.Id);
        
}
}

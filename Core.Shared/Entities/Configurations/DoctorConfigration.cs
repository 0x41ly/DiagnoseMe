using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Shared.Entities.Configurations;

public class DoctorConfigration : BaseConfiguration<Doctor>
{
    public override void Configure(EntityTypeBuilder<Doctor> builder=null!)
    {
        builder.ToTable("Doctors");
        base.Configure(builder);
        builder.Property(c => c.ClinicId).IsRequired();
        builder.Property(c => c.UserId).IsRequired().HasMaxLength(450);
        builder.Property(c => c.Title).IsRequired().HasMaxLength(50);
        builder.Property(c => c.ProfilPic).IsRequired();
        builder.Property(c => c.Specialization).IsRequired().HasMaxLength(50);
        builder.Property(c => c.Bio).IsRequired().HasMaxLength(600);
        builder.Property(c => c.License).IsRequired().HasMaxLength(50);
        builder.HasMany(d => d.DoctorRates).WithOne(dr => dr.Doctor).HasForeignKey(dr => dr.DoctorId);
        
    }
}
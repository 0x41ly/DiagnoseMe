using Auth.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auth.Persistence.Context.Configurations;

public class DoctorConfiguration : BaseConfiguration<Doctor>
{
    public override void Configure(EntityTypeBuilder<Doctor> builder)
    {
        builder.ToTable("Doctors");
        base.Configure(builder);
        builder.Property(d => d.Title).IsRequired().HasMaxLength(50);
        builder.Property(d => d.Specialization).IsRequired().HasMaxLength(50);
        builder.Property(d => d.Bio).IsRequired().HasMaxLength(500);
        builder.Property(d => d.License).IsRequired().HasMaxLength(50);
        builder.Property(d => d.IsLicenseVerified).IsRequired();
        builder.Property(d => d.ClinicId).IsRequired();
    }
}
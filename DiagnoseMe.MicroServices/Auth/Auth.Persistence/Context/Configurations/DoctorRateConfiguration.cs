using Auth.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auth.Persistence.Context.Configurations;

public class DoctorRateConfiguration : BaseConfiguration<DoctorRate>
{
    public override void Configure(EntityTypeBuilder<DoctorRate> builder)
    {
        builder.ToTable("DoctorRates");
        base.Configure(builder);
        builder.Property(dr => dr.Rate).IsRequired();
        builder.Property(dr => dr.PatientId).IsRequired();
        builder.Property(dr => dr.DoctorId).IsRequired();
        builder.HasOne(dr => dr.Doctor).WithMany(d => d.DoctorRates).HasForeignKey(dr => dr.DoctorId);
        builder.HasOne(dr => dr.Patient).WithMany(p => p.DoctorRates).HasForeignKey(dr => dr.PatientId);
    }
}
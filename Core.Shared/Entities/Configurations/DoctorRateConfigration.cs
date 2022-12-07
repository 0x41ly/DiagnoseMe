using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Shared.Entities.Configurations;

public class DoctorRateConfigration : BaseConfiguration<DoctorRate>
{
    public override void Configure(EntityTypeBuilder<DoctorRate> builder=null!)
    {
        builder.ToTable("DoctorRates");
        base.Configure(builder);
        builder.Property(dr => dr.Rate).IsRequired();
        builder.Property(dr => dr.PatientId).IsRequired();
        builder.Property(dr => dr.DoctorId).IsRequired();

        builder.HasOne(dr => dr.Patient).WithMany(p => p.DoctorRates).HasForeignKey(dr => dr.PatientId);
        
    }
}
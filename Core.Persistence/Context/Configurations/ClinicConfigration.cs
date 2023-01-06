using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Persistence.Context.Configurations;

public class ClinicConfigration : BaseConfiguration<Clinic>
{
    public override void Configure(EntityTypeBuilder<Clinic> builder=null!)
    {
        builder.ToTable("Clinics");
        base.Configure(builder);
        builder.Property(c => c.Name).IsRequired().HasMaxLength(50);
        builder.Property(c => c.Specialization).IsRequired().HasMaxLength(50);
        builder.Property(c => c.Address).IsRequired().HasMaxLength(150);
        builder.HasMany(c => c.ClinicPhoneNumbers).WithOne(cpn => cpn.Clinic);
        builder.HasMany(c => c.Doctors).WithOne(d => d.Clinic).HasForeignKey(d => d.ClinicId);
        
    }
}
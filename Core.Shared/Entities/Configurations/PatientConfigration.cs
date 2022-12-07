using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Shared.Entities.Configurations;

public class PatientConfigration : BaseConfiguration<Patient>
{
    public override void Configure(EntityTypeBuilder<Patient> builder=null!)
    {
        builder.ToTable("Patients");
        base.Configure(builder);
        builder.Property(mr => mr.Height).IsRequired();
        builder.Property(mr => mr.Weight).IsRequired();
        builder.Property(c => c.UserId).HasMaxLength(450);
        
        
    }
}
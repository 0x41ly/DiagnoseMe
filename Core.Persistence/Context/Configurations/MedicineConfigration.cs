using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Persistence.Context.Configuration;

public class MedicineConfigration : BaseConfiguration<Medicine>
{
    public override void Configure(EntityTypeBuilder<Medicine> builder=null!)
    {
        builder.ToTable("Medicines");
        base.Configure(builder);
        builder.Property(mr => mr.MedicalRecordId).IsRequired();
        builder.Property(mr => mr.Name).IsRequired().HasMaxLength(50);
        builder.Property(mr => mr.Dose).IsRequired().HasMaxLength(50);
        builder.Property(mr => mr.Concentration).IsRequired().HasMaxLength(10);
        builder.Property(mr => mr.Type).IsRequired().HasMaxLength(15);
        
    }
}
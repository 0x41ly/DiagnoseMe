using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Persistence.Context.Configuration;

public class CheckConfigration :  BaseConfiguration<Check>

{
    public override void  Configure(EntityTypeBuilder<Check> builder=null!)
    {
        builder.ToTable("Checks");
        base.Configure(builder);
        builder.Property(ch =>ch.Name).IsRequired().HasMaxLength(50);
        builder.Property(ch =>ch.Data).IsRequired().HasMaxLength(50);
        builder.Property(ch =>ch.Type).IsRequired().HasMaxLength(10);
        builder.Property(ch =>ch.Report).IsRequired();
        builder.Property(ch =>ch.MedicalRecordId).IsRequired();
        builder.HasOne(ch => ch.MedicalRecord).WithMany(m => m.Checks).HasForeignKey(ch => ch.MedicalRecordId);
    } 
}
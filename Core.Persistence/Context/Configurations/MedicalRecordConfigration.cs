using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Persistence.Context.Configurations;

public class MedicalRecordConfigration : BaseConfiguration<MedicalRecord>
{
    public override void Configure(EntityTypeBuilder<MedicalRecord> builder=null!)
    {
        builder.ToTable("MedicalRecords");
        base.Configure(builder);
        builder.Property(mr => mr.Illness).IsRequired().HasMaxLength(100);
        builder.Property(mr => mr.PatientId).IsRequired();
        builder.Property(mr => mr.DoctorId).IsRequired();
        builder.Property(mr => mr.Diagnoses).IsRequired();


        builder.HasOne(mr => mr.Doctor).WithMany(d => d.MedicalRecords).HasForeignKey(mr => mr.PatientId);
        builder.HasOne(mr => mr.Patient).WithMany(p => p.MedicalRecords).HasForeignKey(mr => mr.DoctorId);
        builder.HasMany(mr => mr.Medicines).WithOne(m => m.MedicalRecord).HasForeignKey(mr => mr.MedicalRecordId);
        
    }
}
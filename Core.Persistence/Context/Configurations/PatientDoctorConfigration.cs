using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Persistence.Context.Configurations;

public class PatientDoctorsConfigration :  IEntityTypeConfiguration<PatientDoctor> 

{
    public void Configure(EntityTypeBuilder<PatientDoctor> builder=null!)
    {
        builder.ToTable("PatientDoctors");
        builder.HasKey(pd => new {pd.DoctorId, pd.PatientId});
        builder.Property(dp => dp.DoctorId).IsRequired();
        builder.Property(dp => dp.PatientId).IsRequired();
        
        builder.HasOne(dp => dp.Doctor).WithMany(dp => dp.PatientDoctors).HasForeignKey(dp => dp.DoctorId);
        builder.HasOne(dp => dp.Patient).WithMany(dp => dp.PatientDoctors).HasForeignKey(dp => dp.PatientId);
    }
}
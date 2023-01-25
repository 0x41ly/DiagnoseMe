namespace Auth.Persistence.Context.Configurations;

using Auth.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class PatientDoctorConfiguration : IEntityTypeConfiguration<PatientDoctor>
{
    public void Configure(EntityTypeBuilder<PatientDoctor> builder)
    {
        builder.ToTable("PatientDoctors");
        builder.HasKey(pd => new {pd.PatientId, pd.DoctorId});
        builder.HasOne(pd => pd.Patient).WithMany(p => p.PatientDoctors).HasForeignKey(pd => pd.PatientId);
        builder.HasOne(pd => pd.Doctor).WithMany(d => d.PatientDoctors).HasForeignKey(pd => pd.DoctorId);
    }
}
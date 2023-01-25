using Auth.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auth.Persistence.Context.Configurations;

public class PatientConfiguration : BaseConfiguration<Patient>
{
    public override void Configure(EntityTypeBuilder<Patient> builder)
    {
        builder.ToTable("Patients");
        base.Configure(builder);
        builder.Property(p => p.Height).IsRequired();
        builder.Property(p => p.Weight).IsRequired();
    }
}
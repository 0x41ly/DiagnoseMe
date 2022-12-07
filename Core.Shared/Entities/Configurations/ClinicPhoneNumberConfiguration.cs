using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Shared.Entities.Configurations;

public class ClinicPhoneNumbersConfigration :  IEntityTypeConfiguration<ClinicPhoneNumber> 

{
    public void Configure(EntityTypeBuilder<ClinicPhoneNumber> builder=null!)
    {
        builder.ToTable("ClinicPhoneNumbers");
        builder.HasKey(cpn => new {cpn.ClinicId, cpn.PhoneNumber});
        builder.Property(cpn => cpn.PhoneNumber).IsRequired().HasMaxLength(16);
        builder.Property(cpn => cpn.ClinicId).IsRequired();
        builder.Property(e => e.CreationDate).HasDefaultValue(DateTime.Now);
        builder.Property(e => e.ConcurrencyStamp).IsRowVersion();
    }
}
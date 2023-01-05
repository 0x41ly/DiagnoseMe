using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Persistence.Context.Configuration;

public class ApplicationUserConfigration :  IEntityTypeConfiguration<ApplicationUser> 

{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder=null!)
    {
        builder.Property(au => au.FirstName).IsRequired().HasMaxLength(50);
        builder.Property(au => au.LastName).IsRequired().HasMaxLength(50); 
        builder.Property(au => au.Gender).IsRequired().HasMaxLength(50);
        builder.Property(au => au.BloodType).IsRequired().HasMaxLength(5);
        builder.Property(au => au.DAteOfBirth).IsRequired();
        builder.Property(au => au.NationalID).IsRequired().HasMaxLength(15);
        builder.Property(au => au.LastConfirmationSentDate).HasDefaultValue(DateTime.Now);
        builder.Property(au => au.IsDoctor).IsRequired();
        builder.HasOne(au => au.Doctor).WithOne(d => d.User).HasForeignKey<Doctor>(d => d.UserId);
        builder.HasOne(au => au.Patient).WithOne(p => p.User).HasForeignKey<Patient>(p => p.UserId);
    }
}
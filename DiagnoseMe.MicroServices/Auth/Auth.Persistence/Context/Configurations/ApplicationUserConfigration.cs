using Auth.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auth.Persistence.Context.Configuration;

public class ApplicationUserConfigration :  IEntityTypeConfiguration<ApplicationUser> 

{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder=null!)
    {
        builder.Property(au => au.FirstName).IsRequired().HasMaxLength(50);
        builder.Property(au => au.LastName).IsRequired().HasMaxLength(50); 
        builder.Property(au => au.Gender).IsRequired().HasMaxLength(50);
        builder.Property(au => au.BloodType).IsRequired().HasMaxLength(5);
        builder.Property(au => au.DateOfBirth).IsRequired();
        builder.Property(au => au.LastConfirmationSentDate).HasDefaultValue(DateTime.Now);
        builder.Property(au => au.NationalID).IsRequired().HasMaxLength(15);
        builder.Property(au => au.LastEmailChangeDate).HasDefaultValue(DateTime.Now);
        builder.Property(au => au.LastUserNameChangeDate).HasDefaultValue(DateTime.Now);
        builder.Property(au => au.IsDoctor).IsRequired();
        builder.Property(au => au.ProfilePictureUrl).IsRequired().HasMaxLength(150);
    }
}
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace MedicalBlog.Persistence.Context.Configurations;

public class UserConfiguration : BaseConfiguration<User>
{
    public override void Configure(EntityTypeBuilder<User> builder=null!)
    {
        builder.ToTable("Users");
        base.Configure(builder);
        builder.Property(c => c.Name).IsRequired();  
        builder.Property(c => c.ProfilePictureUrl).IsRequired();
        builder.Property(c => c.FullName).IsRequired(); 
    }
}
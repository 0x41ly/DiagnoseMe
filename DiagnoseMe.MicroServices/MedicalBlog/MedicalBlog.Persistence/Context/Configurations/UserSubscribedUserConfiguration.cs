using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedicalBlog.Persistence.Context.Configurations;

public class UserSubscribedUserConfiguration : BaseConfiguration<UserSubscribedUser>
{
    public override void Configure(EntityTypeBuilder<UserSubscribedUser> builder)
    {
        builder.HasKey(x => new { x.UserId, x.SubscribedUserId });
        builder.ToTable("UserSubscribedUsers");
        builder.Property(x => x.UserId).IsRequired();
        builder.Property(x => x.SubscribedUserId).IsRequired();
        builder.HasOne(x => x.User).WithMany(x => x.SubscribedUsers).HasForeignKey(x => x.UserId);
        builder.HasOne(x => x.SubscribedUser).WithMany(x => x.Subscribers).HasForeignKey(x => x.SubscribedUserId);
    }
}


using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Shared.Entities.Configurations
{
    public class PostsConfigration : BaseConfiguration<Post>
    {
        public override void Configure(EntityTypeBuilder<Post> builder=null!)
    {
        builder.ToTable("Posts");
        base.Configure(builder);
        builder.Property(c => c.UserId).IsRequired().HasMaxLength(450);
        builder.Property(mr => mr.Content).IsRequired();
        builder.Property(mr => mr.UserId).IsRequired();
        builder.HasMany(p => p.PostSateSuggestions).WithOne(ps => ps.Post).HasForeignKey(ps =>ps.PostId);
        
    }
    }
}
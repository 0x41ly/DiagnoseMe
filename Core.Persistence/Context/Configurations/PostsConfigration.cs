

using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Persistence.Context.Configuration
{
    public class PostsConfigration : BaseConfiguration<Post>
    {
        public override void Configure(EntityTypeBuilder<Post> builder=null!)
    {
        builder.ToTable("Posts");
        base.Configure(builder);
        builder.Property(c => c.DoctorId).IsRequired();
        builder.Property(mr => mr.Content).IsRequired();
        
        builder.HasMany(p => p.PostStateSuggestions).WithOne(ps => ps.Post).HasForeignKey(ps =>ps.PostId);
        
    }
    }
}
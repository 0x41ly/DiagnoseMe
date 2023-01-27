

using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedicalBlog.Persistence.Context.Configurations;
public class PostConfigration : BaseConfiguration<Post>
{
    public override void Configure(EntityTypeBuilder<Post> builder=null!)
    {
        builder.ToTable("Posts");
        base.Configure(builder);
        builder.Property(c => c.AuthorId).IsRequired();
        builder.Property(mr => mr.Content).IsRequired();
        builder.Property(mr => mr.Title).IsRequired().HasMaxLength(50);
        builder.Property(mr => mr.Tags).IsRequired().HasMaxLength(50);
        builder.Property(mr => mr.ModifiedOn);    
        builder.HasOne(mr => mr.Author).WithMany(u => u.Posts).HasForeignKey(c => c.AuthorId); 


    }
}
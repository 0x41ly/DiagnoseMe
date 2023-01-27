using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedicalBlog.Persistence.Context.Configurations;

public class CommentConfigration : BaseConfiguration<Comment>
{
    public override void Configure(EntityTypeBuilder<Comment> builder=null!)
    {
        builder.ToTable("Comments");
        base.Configure(builder);
        builder.Property(c => c.ParentId).IsRequired();
        builder.Property(c => c.Content).IsRequired();
        builder.Property(c => c.AuthorId).IsRequired();
        builder.Property(c => c.PostId).IsRequired();
        builder.Property(c => c.ModifiedOn);
        builder.HasOne(c => c.ParentComment).WithMany(cm => cm.ChildComments).HasForeignKey(c => c.ParentId);
        builder.HasOne(c => c.Post).WithMany(p => p.Comments).HasForeignKey(c => c.Id); 
        builder.HasOne(c => c.Author).WithMany(u => u.Comments).HasForeignKey(c => c.AuthorId);       
    }
}
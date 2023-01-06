using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Persistence.Context.Configurations;

public class CommentConfigration : BaseConfiguration<Comment>
{
    public override void Configure(EntityTypeBuilder<Comment> builder=null!)
    {
        builder.ToTable("Comments");
        base.Configure(builder);
        builder.Property(c => c.ParentId).IsRequired();
        builder.Property(c => c.Content).IsRequired();
        builder.Property(c => c.OwnerId).IsRequired();
        builder.Property(c => c.PostId).IsRequired();
        builder.HasOne(c => c.ParentComment).WithMany(cm => cm.ChildComments).HasForeignKey(c => c.ParentId);
        builder.HasOne(c => c.Owner).WithMany(o => o.Comments).HasForeignKey(c => c.Id);
        builder.HasOne(c => c.Post).WithMany(p => p.Comments).HasForeignKey(c => c.Id);
        
    }
}
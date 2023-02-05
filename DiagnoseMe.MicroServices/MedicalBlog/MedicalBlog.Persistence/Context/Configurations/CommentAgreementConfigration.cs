using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedicalBlog.Persistence.Context.Configurations;

public class CommentAgreementConfigration : BaseConfiguration<CommentAgreement>
{
    public override void Configure(EntityTypeBuilder<CommentAgreement> builder=null!)
    {
        builder.ToTable("CommentAgreements");
        builder.HasKey(c => new { c.UserId, c.CommentId });
        builder.Property(c => c.IsAgreed).IsRequired();
        builder.Property(c => c.UserId).IsRequired();
        builder.Property(c => c.CommentId).IsRequired();
        builder.HasOne(c => c.Comment).WithMany(c => c.CommentAgreements).HasForeignKey(c => c.Id);
        builder.HasOne(c => c.User).WithMany(u => u.CommentAgreements).HasForeignKey(c => c.UserId);
    }
}
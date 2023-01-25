using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedicalBlog.Persistence.Context.Configurations;

public class CommentAgreementConfigration : BaseConfiguration<CommentAgreement>
{
    public override void Configure(EntityTypeBuilder<CommentAgreement> builder=null!)
    {
        builder.ToTable("CommentAgreements");
        base.Configure(builder);
        builder.Property(c => c.IsAgreed).IsRequired();
        builder.Property(c => c.DoctorId).IsRequired();
        builder.Property(c => c.CommentId).IsRequired();
        builder.HasOne(c => c.Comment).WithMany(c => c.CommentAgreements).HasForeignKey(c => c.Id);
    }
}
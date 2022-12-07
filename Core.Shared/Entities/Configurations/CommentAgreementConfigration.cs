using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Shared.Entities.Configurations;

public class CommentAgreementConfigration : BaseConfiguration<CommentAgreement>
{
    public override void Configure(EntityTypeBuilder<CommentAgreement> builder=null!)
    {
        builder.ToTable("CommentAgreements");
        base.Configure(builder);
        builder.Property(c => c.IsAgreed).IsRequired();
        builder.Property(c => c.DoctorId).IsRequired();
        builder.Property(c => c.CommentId).IsRequired();
        builder.HasOne(c => c.Doctor).WithMany(d => d.CommentAgreements).HasForeignKey(c => c.Id);
        builder.HasOne(c => c.Comment).WithMany(c => c.CommentAgreements).HasForeignKey(c => c.Id);

        
    }
}
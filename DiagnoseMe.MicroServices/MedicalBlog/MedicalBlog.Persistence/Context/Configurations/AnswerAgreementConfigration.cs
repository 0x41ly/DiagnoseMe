using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedicalBlog.Persistence.Context.Configurations;
public class AnswerAgreementsConfigration : BaseConfiguration<AnswerAgreement>
{
    public override void Configure(EntityTypeBuilder<AnswerAgreement> builder=null!)
    {
        builder.ToTable("AnswerAgreements");
        builder.HasKey(c => new { c.AnsweringUserId, c.AnswerId });
        builder.Property(c => c.IsAgreed).IsRequired(); 
        builder.Property(c => c.AnswerId).IsRequired();
        builder.Property(c => c.AnsweringUserId).IsRequired();
        builder.HasOne(c => c.Answer).WithMany(a =>a.AnswerAgreements).HasForeignKey(c => c.AnswerId); 
        builder.HasOne(c => c.AnsweringUser).WithMany(u => u.AnswerAgreements).HasForeignKey(c => c.AnsweringUserId);       
    }
}

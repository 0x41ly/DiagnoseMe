using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedicalBlog.Persistence.Context.Configurations;
public class AnswerAgreementsConfigration : BaseConfiguration<AnswerAgreement>
{
    public override void Configure(EntityTypeBuilder<AnswerAgreement> builder=null!)
    {
        builder.ToTable("AnswerAgreements");
        base.Configure(builder);
        builder.Property(c => c.IsAgreed).IsRequired();
        builder.HasOne(c => c.Answer).WithMany(a =>a.AnswerAgreements).HasForeignKey(c => c.AnswerId);        
    }
}
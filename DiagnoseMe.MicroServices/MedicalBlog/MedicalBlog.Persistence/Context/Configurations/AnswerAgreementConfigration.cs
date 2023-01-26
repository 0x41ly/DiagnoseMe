using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedicalBlog.Persistence.Context.Configurations;
public class AnswerAgreementsConfigration : BaseConfiguration<AnswerAgreement>
{
    public override void Configure(EntityTypeBuilder<AnswerAgreement> builder=null!)
    {
        builder.ToTable("AnswerAgreements");
        base.Configure(builder);
        builder.Property(c => c.IsAgreed).IsRequired(); 
        builder.Property(c => c.AnswerId).IsRequired();
        builder.Property(c => c.UserId).IsRequired();
        builder.HasOne(c => c.Answer).WithMany(a =>a.AnswerAgreements).HasForeignKey(c => c.AnswerId); 
        builder.HasOne(c => c.User).WithMany(u => u.AnswerAgreements).HasForeignKey(c => c.UserId);       
    }
}

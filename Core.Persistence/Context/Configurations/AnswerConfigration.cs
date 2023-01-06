using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Persistence.Context.Configurations;

public class AnswersConfigration : BaseConfiguration<Answer>
    {
    public override void Configure(EntityTypeBuilder<Answer> builder=null!)
    {
        builder.ToTable("Answers");
        base.Configure(builder);
        builder.Property(c => c.AnswerString).IsRequired();
        builder.HasOne(c => c.Question).WithMany(q =>q.Answers).HasForeignKey(c => c.QuestionId);  
        builder.HasOne(c => c.Doctor).WithMany(d =>d.Answers).HasForeignKey(c => c.DoctorId);
        
    }
}

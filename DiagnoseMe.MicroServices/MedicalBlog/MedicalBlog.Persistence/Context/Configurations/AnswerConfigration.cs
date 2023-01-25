using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedicalBlog.Persistence.Context.Configurations;

public class AnswersConfigration : BaseConfiguration<Answer>
    {
    public override void Configure(EntityTypeBuilder<Answer> builder=null!)
    {
        builder.ToTable("Answers");
        base.Configure(builder);
        builder.Property(c => c.AnswerString).IsRequired();
        builder.HasOne(c => c.Question).WithMany(q =>q.Answers).HasForeignKey(c => c.QuestionId);          
    }
}

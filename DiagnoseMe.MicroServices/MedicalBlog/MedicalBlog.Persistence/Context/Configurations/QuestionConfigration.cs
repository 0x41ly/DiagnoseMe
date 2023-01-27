using System.Collections.Immutable;


using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedicalBlog.Persistence.Context.Configurations;

public class QuestionsConfigration : BaseConfiguration<Question>
{
    public override void Configure(EntityTypeBuilder<Question> builder=null!)
    {
        builder.ToTable("Questions");
        base.Configure(builder);
        builder.Property(c => c.QuestionString).IsRequired();  
        builder.Property(c => c.AskingUserId).IsRequired();
        builder.Property(c => c.ModifiedOn);
        builder.HasOne(c => c.AskingUser).WithMany(u => u.Questions).HasForeignKey(c => c.AskingUserId);
    }
}

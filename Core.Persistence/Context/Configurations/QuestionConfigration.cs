

using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Persistence.Context.Configuration
{
    public class QuestionsConfigration : BaseConfiguration<Question>
    {
        public override void Configure(EntityTypeBuilder<Question> builder=null!)
    {
        builder.ToTable("Questions");
        base.Configure(builder);
        builder.Property(c => c.QuestionString).IsRequired();  
        
    }
    }
}
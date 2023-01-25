

using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedicalBlog.Persistence.Context.Configurations;

    public class PostStateSuggestionsConfigration : BaseConfiguration<PostSuggestion>
{
    public override void Configure(EntityTypeBuilder<PostSuggestion> builder=null!)
    {
        builder.ToTable("PostStateSuggestionts");
        base.Configure(builder);
        builder.Property(mr => mr.Type).IsRequired().HasMaxLength(15);
        builder.Property(mr => mr.UserId).IsRequired();        
    }
}

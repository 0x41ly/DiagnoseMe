

using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Persistence.Context.Configuration
{
    public class PostStateSuggestionsConfigration : BaseConfiguration<PostStateSuggestion>
    {
        public override void Configure(EntityTypeBuilder<PostStateSuggestion> builder=null!)
    {
        builder.ToTable("PostStateSuggestionts");
        base.Configure(builder);
        builder.Property(mr => mr.Type).IsRequired().HasMaxLength(15);
        builder.Property(mr => mr.DoctorId).IsRequired();
        builder.Property(mr => mr.PostId).IsRequired();
        
    }
    }
}
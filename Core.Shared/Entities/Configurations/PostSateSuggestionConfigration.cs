

using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Shared.Entities.Configurations
{
    public class PostSateSuggestionsConfigration : BaseConfiguration<PostSateSuggestion>
    {
        public override void Configure(EntityTypeBuilder<PostSateSuggestion> builder=null!)
    {
        builder.ToTable("PostSateSuggestionts");
        base.Configure(builder);
        builder.Property(mr => mr.Type).IsRequired().HasMaxLength(15);
        builder.Property(mr => mr.UserId).IsRequired();
        builder.Property(mr => mr.PostId).IsRequired();
        
    }
    }
}
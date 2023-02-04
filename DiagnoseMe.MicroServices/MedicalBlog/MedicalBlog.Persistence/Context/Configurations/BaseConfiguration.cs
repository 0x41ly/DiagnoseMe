using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedicalBlog.Persistence.Context.Configurations;


public class BaseConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : BaseEntity
{
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).ValueGeneratedOnAdd().HasMaxLength(255);
        builder.Property(e => e.CreatedOn).HasDefaultValue(DateTime.UtcNow);
        builder.Property(e => e.ConcurrencyStamp).IsConcurrencyToken();

    }
}
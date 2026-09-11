using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonalBlog.Domain.Entities.Blog;

namespace PersonalBlog.Infrastructure.Configurations;

public class SubscriptionConfiguration : IEntityTypeConfiguration<Subscription>
{
    public void Configure(EntityTypeBuilder<Subscription> builder)
    {
        builder.ToTable("Subscriptions");
        builder.Property(s => s.Email).HasMaxLength(200).IsRequired();
        builder.HasIndex(s => s.Email).IsUnique();
    }
}

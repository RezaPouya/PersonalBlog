using Infrastructure.Persistence.SqlServer.DbExtensions;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonalBlog.Domain.Entities;

namespace Infrastructure.Persistence.SqlServer.Configurations;

public class SubscriptionConfiguration : IEntityTypeConfiguration<Subscription>
{
    public void Configure(EntityTypeBuilder<Subscription> builder)
    {
        builder.ToTable("Subscriptions");
        builder.ConfigureEntityBase<Subscription>();
        builder.Property(s => s.Email).HasMaxLength(200).IsRequired();
        builder.Property(s => s.Email).HasMaxLength(200).IsRequired();
        builder.Property(s => s.UniqueCode).HasMaxLength(32).IsRequired();
        builder.Property(s => s.UnsubscribedReason).HasMaxLength(128);
        builder.Property(s => s.UnsubscribedDescription).HasMaxLength(512);
        builder.HasIndex(s => new { s.Email, s.IsActive }).IsUnique();
        builder.HasIndex(s => s.UniqueCode).IsUnique();
    }
}

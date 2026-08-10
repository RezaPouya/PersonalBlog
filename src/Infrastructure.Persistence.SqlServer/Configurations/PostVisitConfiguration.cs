using Infrastructure.Persistence.SqlServer.DbExtensions;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonalBlog.Domain.Entities.Posts.Entities;

namespace Infrastructure.Persistence.SqlServer.Configurations;

public class PostVisitConfiguration : IEntityTypeConfiguration<PostVisit>
{
    public void Configure(EntityTypeBuilder<PostVisit> builder)
    {
        builder.ToTable("PostVisits");
        builder.ConfigureEntityBase<PostVisit>();
        builder.Property(v => v.IpAddress).HasMaxLength(45).IsRequired();
        builder.Property(v => v.UserAgent).HasMaxLength(512);
        builder.Property(v => v.Browser).HasMaxLength(100);
        builder.Property(v => v.OS).HasMaxLength(100);
        builder.Property(v => v.Device).HasMaxLength(100);

        builder.HasIndex(v => v.PostId);
        builder.HasIndex(v => v.VisitedAt);

        builder.HasOne(v => v.Post)
            .WithMany(p => p.Visits)
            .HasForeignKey(v => v.PostId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

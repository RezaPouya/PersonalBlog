using Infrastructure.Persistence.SqlServer.DbExtensions;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonalBlog.Domain.Entities.Projects.Entities;

namespace Infrastructure.Persistence.SqlServer.Configurations;

public class ProjectPostConfiguration : IEntityTypeConfiguration<ProjectPost>
{
    public void Configure(EntityTypeBuilder<ProjectPost> builder)
    {
        builder.ToTable("ProjectsPosts");
        builder.ConfigureEntityBase<ProjectPost>();
        builder.Property(c => c.Title).HasMaxLength(200);
        builder.Property(c => c.Description).HasMaxLength(2048);
        builder.Property(c => c.CoverImageUrl).HasMaxLength(2048);

        builder.HasOne(p => p.Project)
            .WithMany(c => c.ProjectPosts)
            .HasForeignKey(p => p.ProjectId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(p => p.Post)
            .WithMany(c => c.ProjectPosts)
            .HasForeignKey(p => p.PostId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}

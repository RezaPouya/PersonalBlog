using Infrastructure.Persistence.SqlServer.DbExtensions;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonalBlog.Domain.Entities;

namespace Infrastructure.Persistence.SqlServer.Configurations;

public class ProjectConfiguration : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        builder.ToTable("Projects");
        builder.ConfigureEntityBase<Project>();
        builder.Property(p => p.Title).HasMaxLength(200).IsRequired();
        builder.Property(p => p.Description).HasMaxLength(2024).IsRequired();
        builder.Property(p => p.ImageUrl).HasMaxLength(2024);
        builder.Property(p => p.LiveUrl).HasMaxLength(2024);
        builder.Property(p => p.RepoUrl).HasMaxLength(2024);


        builder.Property(p => p.Slug).HasMaxLength(200).IsRequired();
        builder.HasIndex(p => p.Slug).IsUnique();

        builder.Property(c => c.TinyUrl).HasMaxLength(10).IsRequired();
        builder.HasIndex(p => p.TinyUrl).IsUnique();
    }
}

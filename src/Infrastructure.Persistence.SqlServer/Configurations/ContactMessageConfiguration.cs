using Infrastructure.Persistence.SqlServer.DbExtensions;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonalBlog.Domain.Entities;

namespace Infrastructure.Persistence.SqlServer.Configurations;

public class ContactMessageConfiguration : IEntityTypeConfiguration<ContactMessage>
{
    public void Configure(EntityTypeBuilder<ContactMessage> builder)
    {
        builder.ToTable("ContactMessages");
        builder.ConfigureEntityBase<ContactMessage>();
        builder.Property(m => m.FullName).HasMaxLength(150).IsRequired();
        builder.Property(m => m.Email).HasMaxLength(200).IsRequired();
        builder.Property(m => m.Subject).HasMaxLength(250).IsRequired();
        builder.Property(m => m.Body).HasMaxLength(4000).IsRequired();
    }
}

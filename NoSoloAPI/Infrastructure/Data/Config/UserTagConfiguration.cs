using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config;

public class UserTagConfiguration : IEntityTypeConfiguration<UserTag>
{
    public void Configure(EntityTypeBuilder<UserTag> builder)
    {
        builder.Property(p => p.Id).IsRequired();
        builder.Property(p => p.Active).IsRequired();
        builder.Property(p => p.Description).IsRequired().HasMaxLength(500);
        builder.Property(p => p.Tag).IsRequired();
    }
}
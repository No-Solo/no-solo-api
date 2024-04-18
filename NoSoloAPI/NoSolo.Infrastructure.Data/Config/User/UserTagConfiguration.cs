using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NoSolo.Core.Entities.User;

namespace NoSolo.Infrastructure.Data.Config.User;

public class UserTagConfiguration : IEntityTypeConfiguration<UserTagEntity>
{
    public void Configure(EntityTypeBuilder<UserTagEntity> builder)
    {
        builder.Property(p => p.Id).IsRequired();
        builder.Property(p => p.Active).IsRequired();
        builder.Property(p => p.Tag).IsRequired().HasMaxLength(20);
    }
}
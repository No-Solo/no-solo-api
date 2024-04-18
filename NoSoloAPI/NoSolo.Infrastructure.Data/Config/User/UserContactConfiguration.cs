using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NoSolo.Core.Entities.Base;

namespace NoSolo.Infrastructure.Data.Config.User;

public class UserContactConfiguration : IEntityTypeConfiguration<ContactEntity<Core.Entities.User.UserEntity>>
{
    public void Configure(EntityTypeBuilder<ContactEntity<Core.Entities.User.UserEntity>> builder)
    {
        builder.Property(p => p.Id).IsRequired();
        builder.Property(p => p.Type).IsRequired().HasMaxLength(50);
        builder.Property(p => p.Url).IsRequired();
        builder.Property(p => p.Text).IsRequired().HasMaxLength(50);
    }
}
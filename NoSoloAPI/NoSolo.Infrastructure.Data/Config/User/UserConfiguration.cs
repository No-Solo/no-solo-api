using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace NoSolo.Infrastructure.Data.Config.User;

public class UserConfiguration : IEntityTypeConfiguration<Core.Entities.User.UserEntity>
{
    public void Configure(EntityTypeBuilder<Core.Entities.User.UserEntity> builder)
    {
        builder.Property(p => p.Id).IsRequired();
        builder.Property(x => x.UserName).IsRequired().HasMaxLength(50);
        builder.Property(x => x.PhoneNumber).HasMaxLength(10);
    }
}
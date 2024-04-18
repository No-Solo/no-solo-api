using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace NoSolo.Infrastructure.Data.Config.User;

public class UserProfileConfiguration : IEntityTypeConfiguration<Core.Entities.User.UserEntity>
{
    public void Configure(EntityTypeBuilder<Core.Entities.User.UserEntity> builder)
    {
        builder.Property(p => p.Id).IsRequired();
        builder.Property(p => p.Description).IsRequired().HasMaxLength(250);
        builder.Property(p => p.About).IsRequired().HasMaxLength(250);
        builder.Property(p => p.Location).IsRequired().HasMaxLength(100);
        builder.Property(p => p.FirstName).IsRequired().HasMaxLength(50);
        builder.Property(p => p.MiddleName).HasMaxLength(50);
        builder.Property(p => p.LastName).HasMaxLength(50);
        builder.Property(p => p.Gender).IsRequired();
        builder.Property(p => p.Locale).IsRequired();
        builder.HasOne(p => p.Photo).WithOne();
    }
}
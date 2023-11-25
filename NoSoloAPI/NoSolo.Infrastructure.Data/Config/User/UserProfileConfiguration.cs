using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace NoSolo.Infrastructure.Data.Config.User;

public class UserProfileConfiguration : IEntityTypeConfiguration<Core.Entities.User.User>
{
    public void Configure(EntityTypeBuilder<Core.Entities.User.User> builder)
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
        builder.HasMany(p => p.Contacts).WithOne()
            .HasForeignKey(x => x.TEntityId);
        builder.HasMany(p => p.Tags).WithOne()
            .HasForeignKey(x => x.UserGuid);
        builder.HasOne(p => p.Photo).WithOne();
        builder.HasMany(p => p.Offers).WithOne()
            .HasForeignKey(x => x.UserGuid);
    }
}
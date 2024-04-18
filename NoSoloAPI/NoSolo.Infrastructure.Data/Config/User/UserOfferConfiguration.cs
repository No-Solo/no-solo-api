using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NoSolo.Core.Entities.User;

namespace NoSolo.Infrastructure.Data.Config.User;

public class UserOfferConfiguration : IEntityTypeConfiguration<UserOfferEntity>
{
    public void Configure(EntityTypeBuilder<UserOfferEntity> builder)
    {
        builder.Property(p => p.Id).IsRequired();
        builder.Property(p => p.Created).IsRequired();
        builder.Property(p => p.Preferences).IsRequired().HasMaxLength(150);
    }
}
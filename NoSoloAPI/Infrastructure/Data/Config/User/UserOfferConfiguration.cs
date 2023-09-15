using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config.User;

public class UserOfferConfiguration : IEntityTypeConfiguration<UserOffer>
{
    public void Configure(EntityTypeBuilder<UserOffer> builder)
    {
        builder.Property(p => p.Id).IsRequired();
        builder.Property(p => p.Created).IsRequired();
        builder.Property(p => p.Preferences).IsRequired().HasMaxLength(150);
    }
}
using NoSolo.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NoSolo.Core.Entities.User;

namespace NoSolo.Infrastructure.Data.Config.User;

public class UserPhotoConfiguration : IEntityTypeConfiguration<UserPhoto>
{
    public void Configure(EntityTypeBuilder<UserPhoto> builder)
    {
        builder.Property(p => p.Id).IsRequired();
        builder.Property(p => p.Url).IsRequired();
    }
}
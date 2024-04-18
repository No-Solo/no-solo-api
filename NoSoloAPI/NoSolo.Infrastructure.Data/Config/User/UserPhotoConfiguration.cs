using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NoSolo.Core.Entities.User;

namespace NoSolo.Infrastructure.Data.Config.User;

public class UserPhotoConfiguration : IEntityTypeConfiguration<UserPhotoEntity>
{
    public void Configure(EntityTypeBuilder<UserPhotoEntity> builder)
    {
        builder.Property(p => p.Id).IsRequired();
        builder.Property(p => p.Url).IsRequired();
    }
}
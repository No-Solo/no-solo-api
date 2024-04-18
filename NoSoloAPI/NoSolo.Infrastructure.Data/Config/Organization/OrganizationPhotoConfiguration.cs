using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NoSolo.Core.Entities.Organization;

namespace NoSolo.Infrastructure.Data.Config.Organization;

public class OrganizationPhotoConfiguration : IEntityTypeConfiguration<OrganizationPhotoEntity>
{
    public void Configure(EntityTypeBuilder<OrganizationPhotoEntity> builder)
    {
        builder.Property(p => p.Id).IsRequired();
        builder.Property(p => p.Url).IsRequired();
        builder.Property(p => p.IsMain).IsRequired();
    }
}
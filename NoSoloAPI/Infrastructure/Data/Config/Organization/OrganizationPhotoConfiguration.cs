using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config.Organization;

public class OrganizationPhotoConfiguration : IEntityTypeConfiguration<OrganizationPhoto>
{
    public void Configure(EntityTypeBuilder<OrganizationPhoto> builder)
    {
        builder.Property(p => p.Id).IsRequired();
        builder.Property(p => p.Url).IsRequired();
        builder.Property(p => p.IsMain).IsRequired();
    }
}
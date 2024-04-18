using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NoSolo.Core.Entities.Organization;

namespace NoSolo.Infrastructure.Data.Config.Organization;

public class OrganizationOfferConfiguration : IEntityTypeConfiguration<OrganizationOfferEntity>
{
    public void Configure(EntityTypeBuilder<OrganizationOfferEntity> builder)
    {
        builder.Property(p => p.Name).IsRequired().HasMaxLength(100);
        builder.Property(p => p.Description).IsRequired().HasMaxLength(500);
        builder.Property(p => p.Created).IsRequired();
    }
}
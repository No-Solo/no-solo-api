using NoSolo.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NoSolo.Core.Entities.Organization;

namespace NoSolo.Infrastructure.Data.Config;

public class OrganizationOfferConfiguration : IEntityTypeConfiguration<OrganizationOffer>
{
    public void Configure(EntityTypeBuilder<OrganizationOffer> builder)
    {
        builder.Property(p => p.Name).IsRequired().HasMaxLength(100);
        builder.Property(p => p.Description).IsRequired().HasMaxLength(500);
        builder.Property(p => p.Created).IsRequired();
    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace NoSolo.Infrastructure.Data.Config.Organization;

public class OrganizationConfiguration : IEntityTypeConfiguration<NoSolo.Core.Entities.Organization.OrganizationEntity>
{
    public void Configure(EntityTypeBuilder<NoSolo.Core.Entities.Organization.OrganizationEntity> builder)
    {
        builder.Property(p => p.Id).IsRequired();
        builder.Property(p => p.Name).IsRequired().HasMaxLength(100);
        builder.Property(p => p.DateCreated).IsRequired();
    }
}
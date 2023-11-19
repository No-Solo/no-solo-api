using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace NoSolo.Infrastructure.Data.Config.Organization;

public class OrganizationConfiguration : IEntityTypeConfiguration<NoSolo.Core.Entities.Organization.Organization>
{
    public void Configure(EntityTypeBuilder<NoSolo.Core.Entities.Organization.Organization> builder)
    {
        builder.Property(p => p.Id).IsRequired();
        builder.Property(p => p.Name).IsRequired().HasMaxLength(100);
        builder.Property(p => p.Created).IsRequired();
        builder.HasMany(p => p.Offers).WithOne()
            .HasForeignKey(x => x.OrganizationId);
        builder.HasMany(p => p.Contacts).WithOne()
            .HasForeignKey(x => x.TEntityId);
        builder.HasMany(p => p.Photos).WithOne()
            .HasForeignKey(x => x.OrganizationId);

        builder.HasOne(p => p.Project).WithOne();
    }
}
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config.Organization;

public class ProjectConfiguration : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        builder.Property(p => p.Id).IsRequired();
        builder.Property(p => p.Name).HasMaxLength(100);
        builder.Property(p => p.Description).IsRequired().HasMaxLength(500);
    }
}
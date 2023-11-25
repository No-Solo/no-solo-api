﻿using NoSolo.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NoSolo.Core.Entities.Organization;

namespace NoSolo.Infrastructure.Data.Data.Config.Organization;

public class OrganizationPhotoConfiguration : IEntityTypeConfiguration<OrganizationPhoto>
{
    public void Configure(EntityTypeBuilder<OrganizationPhoto> builder)
    {
        builder.Property(p => p.Id).IsRequired();
        builder.Property(p => p.Url).IsRequired();
        builder.Property(p => p.IsMain).IsRequired();
    }
}
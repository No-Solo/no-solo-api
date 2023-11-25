﻿using NoSolo.Core.Entities;
using NoSolo.Core.Entities.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace NoSolo.Infrastructure.Data.Data.Config.Organization;

public class OrganizationContactConfiguration : IEntityTypeConfiguration<Contact<NoSolo.Core.Entities.Organization.Organization>>
{
    public void Configure(EntityTypeBuilder<Contact<NoSolo.Core.Entities.Organization.Organization>> builder)
    {
        builder.Property(p => p.Id).IsRequired();
        builder.Property(p => p.Type).IsRequired().HasMaxLength(50);
        builder.Property(p => p.Url).IsRequired();
        builder.Property(p => p.Text).IsRequired().HasMaxLength(50);
    }
}
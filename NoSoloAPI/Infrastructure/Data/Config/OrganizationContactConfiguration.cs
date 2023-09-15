﻿using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config;

public class OrganizationContactConfiguration : IEntityTypeConfiguration<Contact<Organization>>
{
    public void Configure(EntityTypeBuilder<Contact<Organization>> builder)
    {
        builder.Property(p => p.Id).IsRequired();
        builder.Property(p => p.Type).IsRequired().HasMaxLength(50);
        builder.Property(p => p.Url).IsRequired();
        builder.Property(p => p.Text).IsRequired().HasMaxLength(50);
    }
}
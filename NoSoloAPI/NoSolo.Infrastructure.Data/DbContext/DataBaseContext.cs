using System.Reflection;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NoSolo.Core.Entities.Auth;
using NoSolo.Core.Entities.Organization;
using NoSolo.Core.Entities.User;

namespace NoSolo.Infrastructure.Data.DbContext;

public class DataBaseContext(DbContextOptions<DataBaseContext> options)
    : IdentityDbContext<UserEntity, UserRoleEntity, Guid>(options)
{
    public new DbSet<UserEntity> Users { get; set; } = null!;
    public DbSet<RefreshToken> RefreshTokens { get; set; } = null!;
    public DbSet<UserTagEntity> UserTags { get; set; } = null!;
    public DbSet<UserOfferEntity> UserOffers { get; set; } = null!;
    public DbSet<OrganizationEntity> Organizations { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        // UserEntity
        builder.Entity<UserEntity>()
            .HasOne(e => e.Photo)
            .WithOne(e => e.User)
            .HasForeignKey<UserPhotoEntity>(e => e.UserGuid);

        if (Database.ProviderName == "Npgsql.EntityFrameworkCore.PostgreSQL")
            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                var properties = entityType.ClrType.GetProperties().Where(p => p.PropertyType == typeof(decimal));
                var dateTimeProperties = entityType.ClrType.GetProperties()
                    .Where(p => p.PropertyType == typeof(DateTimeOffset));

                foreach (var property in properties)
                    builder.Entity(entityType.Name).Property(property.Name).HasConversion<double>();

                foreach (var property in dateTimeProperties)
                    builder.Entity(entityType.Name).Property(property.Name)
                        .HasConversion(new DateTimeOffsetToBinaryConverter());
            }
    }
}
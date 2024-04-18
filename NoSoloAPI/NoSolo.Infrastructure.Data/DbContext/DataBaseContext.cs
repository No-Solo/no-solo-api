using System.Reflection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NoSolo.Core.Entities.Auth;
using NoSolo.Core.Entities.FeedBack;
using NoSolo.Core.Entities.Organization;
using NoSolo.Core.Entities.User;

namespace NoSolo.Infrastructure.Data.DbContext;

public class DataBaseContext : IdentityDbContext<UserEntity, UserRoleEntity, Guid>
{
    public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
    {
    }

    public DbSet<UserEntity> Users { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<UserTagEntity> UserTags { get; set; }
    public DbSet<UserOfferEntity> UserOffers { get; set; }
    public DbSet<OrganizationEntity> Organizations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        // UserEntity
        modelBuilder.Entity<UserEntity>()
            .HasOne(e => e.Photo)
            .WithOne(e => e.User)
            .HasForeignKey<UserPhotoEntity>(e => e.UserGuid);

        if (Database.ProviderName == "Npgsql.EntityFrameworkCore.PostgreSQL")
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var properties = entityType.ClrType.GetProperties().Where(p => p.PropertyType == typeof(decimal));
                var dateTimeProperties = entityType.ClrType.GetProperties()
                    .Where(p => p.PropertyType == typeof(DateTimeOffset));

                foreach (var property in properties)
                    modelBuilder.Entity(entityType.Name).Property(property.Name).HasConversion<double>();

                foreach (var property in dateTimeProperties)
                    modelBuilder.Entity(entityType.Name).Property(property.Name)
                        .HasConversion(new DateTimeOffsetToBinaryConverter());
            }
    }
}
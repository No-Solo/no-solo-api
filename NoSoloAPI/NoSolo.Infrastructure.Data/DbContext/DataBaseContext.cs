using System.Reflection;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NoSolo.Core.Entities.Auth;
using NoSolo.Core.Entities.Organization;
using NoSolo.Core.Entities.User;

namespace NoSolo.Infrastructure.Data.DbContext;

public class DataBaseContext : IdentityDbContext<User, UserRole, Guid>
{
    public DataBaseContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<UserTag> UserTags { get; set; }
    public DbSet<UserOffer> UserOffers { get; set; }
    public DbSet<Organization> Organizations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        // User Profile
        modelBuilder.Entity<User>()
            .HasOne(e => e.Photo)
            .WithOne(e => e.User)
            .HasForeignKey<UserPhoto>(e => e.UserGuid);
        
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
using System.Reflection;
using Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Data;

public class DataBaseContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
{
    public DataBaseContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Organization> Organizations { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    
    public DbSet<UserProfile> UserProfiles { get; set; }
    public DbSet<UserTag> UserTags { get; set; }
    public DbSet<UserOffer> UserOffers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        // User Profile
        modelBuilder.Entity<UserProfile>()
            .HasOne(e => e.Photo)
            .WithOne(e => e.UserProfile)
            .HasForeignKey<UserPhoto>(e => e.UserProfileId);


        // User
        modelBuilder.Entity<User>()
            .HasOne(e => e.UserProfile)
            .WithOne(e => e.User)
            .HasForeignKey<UserProfile>(e => e.UserId);


        // Organization
        modelBuilder.Entity<Organization>()
            .HasOne(e => e.Project)
            .WithOne(e => e.Organization)
            .HasForeignKey<Project>(e => e.OrganizationId);

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
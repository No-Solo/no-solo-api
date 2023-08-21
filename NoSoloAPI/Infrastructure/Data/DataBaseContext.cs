using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Data;

public class DataBaseContext : DbContext
{
    public DataBaseContext(DbContextOptions options) : base(options)
    {
    }
    
    public DbSet<User> Users { get; set; }
    public DbSet<Organization> Organizations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);


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



        if (Database.ProviderName == "Microsoft.EntityFrameworkCore.Sqlite")
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var properties = entityType.ClrType.GetProperties().Where(p => p.PropertyType == typeof(decimal));
                var dateTimeProperties = entityType.ClrType.GetProperties()
                    .Where(p => p.PropertyType == typeof(DateTimeOffset));

                foreach (var property in properties)
                {
                    modelBuilder.Entity(entityType.Name).Property(property.Name).HasConversion<double>();
                }

                foreach (var property in dateTimeProperties)
                {
                    modelBuilder.Entity(entityType.Name).Property(property.Name)
                        .HasConversion(new DateTimeOffsetToBinaryConverter());
                }
            }
        }
    }
}
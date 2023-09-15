using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config.User;

public class UserConfiguration : IEntityTypeConfiguration<Core.Entities.User>
{
    public void Configure(EntityTypeBuilder<Core.Entities.User> builder)
    {
        builder.Property(p => p.Id).IsRequired();
        builder.Property(x => x.UserName).IsRequired().HasMaxLength(50);
        builder.Property(x => x.PhoneNumber).HasMaxLength(10);
    }
}
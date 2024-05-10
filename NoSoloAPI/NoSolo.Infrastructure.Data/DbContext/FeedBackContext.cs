using Microsoft.EntityFrameworkCore;

namespace NoSolo.Infrastructure.Data.DbContext;

public class FeedBackContext(DbContextOptions<FeedBackContext> options)
    : Microsoft.EntityFrameworkCore.DbContext(options)
{
    public DbSet<Core.Entities.FeedBack.FeedBackEntity> FeedBacks { get; set; }
}
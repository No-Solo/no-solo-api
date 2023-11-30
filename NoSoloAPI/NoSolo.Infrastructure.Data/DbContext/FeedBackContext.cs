using Microsoft.EntityFrameworkCore;

namespace NoSolo.Infrastructure.Data.DbContext;

public class FeedBackContext : Microsoft.EntityFrameworkCore.DbContext
{
    public FeedBackContext(DbContextOptions<FeedBackContext> options) : base(options)
    {
        
    }
    
    public DbSet<Core.Entities.FeedBack.FeedBack> FeedBacks { get; set; }
}
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class DataBaseContext : DbContext
{
    public DataBaseContext(DbContextOptions options) : base(options)
    {
    }
}
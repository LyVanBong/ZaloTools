using Microsoft.EntityFrameworkCore;

namespace ZaloTools.Database;

public class DatabaseLocalContext : DbContext
{
    public DbSet<AccountZalo> AccountZalos { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=ZaloToolDb.db");
        optionsBuilder.UseLazyLoadingProxies();
    }
}
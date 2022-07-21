namespace ZaloTools.Database;

public class DatabaseLocalContext : DbContext
{
    public DbSet<AccountZalo> AccountZalos { get; set; }
    public DbSet<MessagesFriends> MessagesFriends { get; set; }
    public DbSet<Friend> Friends { get; set; }
    public DbSet<SettingMessage> SettingMessages { get; set; }
    public DbSet<ContentMessage> ContentMessages { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=ZaloToolDb.db");
        optionsBuilder.UseLazyLoadingProxies();
    }
}
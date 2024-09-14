namespace ModularMonolith.Shared.Persistance
{
    public class DbOptions
    {
        public string? ReadConnectionString { get; set; }
        public string? ConnectionString { get; set; }
        public DbMigrator Migrator { get; set; } = new DbMigrator();
    }

    public class DbMigrator
    {
        public bool IsEnabled { get; set; }
    }
}

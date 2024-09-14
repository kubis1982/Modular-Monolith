namespace ModularMonolith.Shared.Persistance.WriteModel
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Design;

    public abstract class WriteDbContextFactoryBase<TDbContext> : IDesignTimeDbContextFactory<TDbContext> where TDbContext : WriteDbContextBase
    {
        public TDbContext CreateDbContext(string[] args)
        {
            DbContextOptionsBuilder<TDbContext> dbContextOptionsBuilder = new();
            dbContextOptionsBuilder.UseNpgsql("User ID=postgres;Password=mypassword;Host=localhost;Port=5432;Database=Kubis1982", n => {
                n.MigrationsHistoryTable("MigrationHistory", Schema);
            });
            return CreateDbContext(dbContextOptionsBuilder.Options);
        }

        protected abstract TDbContext CreateDbContext(DbContextOptions<TDbContext> dbContextOptions);

        protected abstract string? Schema { get; }
    }
}

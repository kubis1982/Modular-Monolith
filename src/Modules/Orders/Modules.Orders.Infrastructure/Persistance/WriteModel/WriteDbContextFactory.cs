namespace ModularMonolith.Modules.Ordering.Persistance.WriteModel
{
    using Microsoft.EntityFrameworkCore;

    public class WriteDbContextFactory : WriteDbContextFactoryBase<WriteDbContext>
    {
        protected override WriteDbContext CreateDbContext(DbContextOptions<WriteDbContext> dbContextOptions)
        {
            return new WriteDbContext(dbContextOptions);
        }

        protected override string? Schema => EntityType.ModuleCode;
    }
}

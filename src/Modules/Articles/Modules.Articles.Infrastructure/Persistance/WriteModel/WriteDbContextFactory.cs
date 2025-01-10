namespace ModularMonolith.Modules.Articles.Persistance.WriteModel
{
    using Microsoft.EntityFrameworkCore;

    public class WriteDbContextFactory : WriteDbContextFactoryBase<WriteDbContext>
    {
        protected override string? Schema => EntityType.ModuleCode;

        protected override WriteDbContext CreateDbContext(DbContextOptions<WriteDbContext> dbContextOptions)
        {
            return new WriteDbContext(dbContextOptions);
        }
    }
}

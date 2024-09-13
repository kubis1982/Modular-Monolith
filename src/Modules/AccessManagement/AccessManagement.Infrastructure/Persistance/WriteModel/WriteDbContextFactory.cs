namespace Kubis1982.Modules.AccessManagement.Persistance.WriteModel
{
    public class WriteDbContextFactory : WriteDbContextFactoryBase<WriteDbContext>
    {
        protected override WriteDbContext CreateDbContext(DbContextOptions<WriteDbContext> dbContextOptions)
        {
            return new WriteDbContext(dbContextOptions);
        }

        protected override string? Schema => EntityType.ModuleCode;
    }
}

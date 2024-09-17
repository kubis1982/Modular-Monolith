namespace ModularMonolith.Modules.ProjectName.Persistance.WriteModel
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

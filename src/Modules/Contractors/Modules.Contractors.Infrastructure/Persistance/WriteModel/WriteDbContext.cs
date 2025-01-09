namespace ModularMonolith.Modules.Contractors.Persistance.WriteModel
{
    public class WriteDbContext(DbContextOptions<WriteDbContext> options) : WriteDbContextBase(options)
    {
        protected override string Schema => EntityType.ModuleCode;
    }
}

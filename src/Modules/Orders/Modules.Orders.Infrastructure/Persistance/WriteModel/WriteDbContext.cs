namespace ModularMonolith.Modules.Ordering.Persistance.WriteModel
{
    public class WriteDbContext : WriteDbContextBase
    {
        public WriteDbContext(DbContextOptions<WriteDbContext> options) : base(options)
        {
        }

        protected override string Schema => EntityType.ModuleCode;
    }
}

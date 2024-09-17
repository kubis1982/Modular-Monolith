namespace ModularMonolith.Modules.ProjectName.Persistance.WriteModel
{
    using Microsoft.EntityFrameworkCore;
    using ModularMonolith.Modules.ProjectName.Domain;
    using ModularMonolith.Shared.Persistance.WriteModel;

    public class WriteDbContext(DbContextOptions<WriteDbContext> options) : WriteDbContextBase(options)
    {
        protected override string Schema => EntityType.ModuleCode;
    }
}

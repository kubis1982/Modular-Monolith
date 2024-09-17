namespace ModularMonolith.Modules.ProjectName.Persistance.ReadModel
{
    using Microsoft.EntityFrameworkCore;
    using ModularMonolith.Shared.Persistance.ReadModel;

    public class ReadDbContext(DbContextOptions<ReadDbContext> options) : ReadDbContextBase(options)
    {

    }
}

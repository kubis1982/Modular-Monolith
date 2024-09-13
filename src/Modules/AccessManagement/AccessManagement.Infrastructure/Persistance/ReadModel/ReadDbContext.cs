namespace Kubis1982.Modules.AccessManagement.Persistance.ReadModel
{
    using Kubis1982.Shared.Persistance.ReadModel;
    using Microsoft.EntityFrameworkCore;

    public class ReadDbContext(DbContextOptions<ReadDbContext> options) : ReadDbContextBase(options)
    {
    }
}

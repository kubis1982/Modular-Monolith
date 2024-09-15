namespace ModularMonolith.Modules.AccessManagement.Persistance.ReadModel
{
    using ModularMonolith.Shared.Persistance.ReadModel;
    using Microsoft.EntityFrameworkCore;

    public class ReadDbContext(DbContextOptions<ReadDbContext> options) : ReadDbContextBase(options)
    {
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<SessionEntity> Sessions { get; set; }
    }
}

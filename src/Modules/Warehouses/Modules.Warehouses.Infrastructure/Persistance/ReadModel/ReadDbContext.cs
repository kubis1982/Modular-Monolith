namespace ModularMonolith.Modules.Warehouses.Persistance.ReadModel
{
    using ModularMonolith.Modules.Warehouses.Persistance.ReadModel.Entities;
    using ModularMonolith.Shared.Persistance.ReadModel;

    public class ReadDbContext(DbContextOptions<ReadDbContext> options) : ReadDbContextBase(options)
    {
        public virtual DbSet<WarehouseEntity> Warehouses { get; set; } = null!;
    }
}

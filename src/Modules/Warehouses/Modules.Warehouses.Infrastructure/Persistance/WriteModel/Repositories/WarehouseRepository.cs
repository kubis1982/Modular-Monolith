namespace ModularMonolith.Modules.Warehouses.Persistance.WriteModel.Repositories
{
    using ModularMonolith.Modules.Warehouses.Domain.Warehouses;

    class WarehouseRepository(WriteDbContext dbContext) : Repository<Warehouse, WarehouseSpec>(dbContext), IWarehouseRepository
    {
    }
}

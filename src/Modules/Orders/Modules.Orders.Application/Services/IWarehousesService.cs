namespace ModularMonolith.Modules.Ordering.Services
{
    using ModularMonolith.Modules.Ordering.Domain;

    public interface IWarehousesService
    {
        Task<Warehouse> GetWarehouseAsync(int warehouseId, CancellationToken cancellationToken);
    }
}

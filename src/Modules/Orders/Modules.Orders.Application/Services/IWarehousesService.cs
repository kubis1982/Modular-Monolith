namespace ModularMonolith.Modules.Orders.Services
{
    using ModularMonolith.Modules.Orders.Domain;

    public interface IWarehousesService
    {
        Task<Warehouse> GetWarehouseAsync(int warehouseId, CancellationToken cancellationToken);
    }
}

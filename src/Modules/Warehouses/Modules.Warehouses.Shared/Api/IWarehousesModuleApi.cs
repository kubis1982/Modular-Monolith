namespace ModularMonolith.Modules.Warehouses.Api
{
    public interface IWarehousesModuleApi
    {
        Task<WarehouseDto> GetWarehouseAsync(int warehouseId, CancellationToken cancellationToken);
        Task<WarehouseDto[]> GetWarehousesAsync(int[] warehouseIds, CancellationToken cancellationToken);
    }
}

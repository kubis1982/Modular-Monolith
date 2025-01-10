namespace ModularMonolith.Modules.Ordering.Services
{
    using AutoMapper;
    using ModularMonolith.Modules.Warehouses.Api;
    using System.Threading;
    using System.Threading.Tasks;

    internal class WarehousesService(IWarehousesModuleApi warehousesModuleApi, IMapper mapper) : IWarehousesService
    {
        public async Task<Warehouse> GetWarehouseAsync(int warehouseId, CancellationToken cancellationToken)
        {
            return mapper.Map<Warehouse>(await warehousesModuleApi.GetWarehouseAsync(warehouseId, cancellationToken));
        }
    }
}

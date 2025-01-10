namespace ModularMonolith.Modules.Warehouses.Api
{
    using ModularMonolith.Modules.Warehouses.Api.Queries;
    using ModularMonolith.Modules.Warehouses.Domain.Warehouses.Exceptions;
    using ModularMonolith.Shared.CQRS.Queries;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    internal class WarehousesModuleApi(IQueryExecutor queryExecutor) : IWarehousesModuleApi
    {
        public async Task<WarehouseDto> GetWarehouseAsync(int warehouseId, CancellationToken cancellationToken)
        {
            return (await queryExecutor.Execute(new GetWarehousesQuery([warehouseId]), default)).FirstOrDefault()
                ?? throw new WarehouseNotFoundException(warehouseId);
        }

        public async Task<WarehouseDto[]> GetWarehousesAsync(int[] warehouseIds, CancellationToken cancellationToken)
        {
            return await queryExecutor.Execute(new GetWarehousesQuery(warehouseIds), default);
        }
    }
}

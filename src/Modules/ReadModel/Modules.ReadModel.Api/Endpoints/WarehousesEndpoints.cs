namespace ModularMonolith.Modules.ReadModel.Endpoints
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Http.HttpResults;
    using Microsoft.AspNetCore.Mvc;
    using ModularMonolith.Modules.ReadModel.Queries.Warehouses;
    using ModularMonolith.Shared.CQRS.Queries;
    using System.Threading;
    using System.Threading.Tasks;

    internal class WarehousesEndpoints() : ModuleEndpoints(Modules.Warehouses) {
        public override void AddRoutes(IModuleEndpointRouteBuilder endpointRouteBuilder) {
            endpointRouteBuilder.MapGet("warehouses/{warehouseId}", GetWarehouse);

        }

        private async Task<Results<Ok<GetWarehouseQueryResult>, NotFound>> GetWarehouse([FromServices] IQueryExecutor queryExecutor, [FromRoute] int warehouseId, CancellationToken cancellationToken) {
            var result = await queryExecutor.Execute(new GetWarehouseQuery(warehouseId), cancellationToken);
            return result switch {
                null => TypedResults.NotFound(),
                _ => TypedResults.Ok(result)
            };
        }
    }
}

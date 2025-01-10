using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using ModularMonolith.Modules.Warehouses.Commands.Warehouses;
using ModularMonolith.Modules.Warehouses.Queries.Warehoues;
using ModularMonolith.Modules.Warehouses.Requests.Warehouses;
using ModularMonolith.Shared.CQRS.Commands;
using ModularMonolith.Shared.CQRS.Queries;
using ModularMonolith.Shared.Modules;
using ModularMonolith.Shared.Modules.Endpoints.Responses;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ModularMonolith.Modules.Warehouses;

public class Endpoints : IModuleEndpoints
{
    public void AddRoutes(IEndpointRouteBuilder endpointRouteBuilder)
    {
        endpointRouteBuilder.MapPost("warehouses", CreateWarehouse);
        endpointRouteBuilder.MapPut("warehouses/{warehouseId:int}", UpdateWarehouse);
        endpointRouteBuilder.MapDelete("warehouses/{warehouseId}", DeleteWarehouse);
        endpointRouteBuilder.MapPatch("warehouses/{warehouseId}/block", BlockWarehouse);
        endpointRouteBuilder.MapPatch("warehouses/{warehouseId}/unblock", UnblockWarehouse);
        endpointRouteBuilder.MapGet("warehouses", GetWarehouses);
        endpointRouteBuilder.MapGet("warehouses/{warehouseId:int}", GetWarehouse);
    }

    /// <summary>
    /// Gets a warehouse
    /// </summary>
    /// <param name="queryExecutor"></param>
    /// <param name="warehouseId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    private async Task<Results<Ok<GetWarehousesQueryResult>, NotFound>> GetWarehouse([FromServices] IQueryExecutor queryExecutor, [FromRoute] int warehouseId, CancellationToken cancellationToken)
    {
        var result = (await queryExecutor.Execute(new GetWarehousesQuery(warehouseId), cancellationToken)).FirstOrDefault();
        return result switch
        {
            null => TypedResults.NotFound(),
            _ => TypedResults.Ok(result)
        };
    }

    /// <summary>
    /// Gets all warehouses
    /// </summary>
    /// <param name="queryExecutor"></param>
    /// <param name="arguments"></param>
    /// <param name="cancellationToken"></param>
    private async Task<Ok<IEnumerable<GetWarehousesQueryResult>>> GetWarehouses([FromServices] IQueryExecutor queryExecutor, [AsParameters] GetWarehousesRequest arguments, CancellationToken cancellationToken)
    {
        var results = await queryExecutor.Execute(new GetWarehousesQuery(arguments.IncludeBlocked ?? false), cancellationToken);
        return TypedResults.Ok(results);
    }

    /// <summary>
    /// Unblocks a warehouse
    /// </summary>
    /// <param name="commandExecutor"></param>
    /// <param name="warehouseId"></param>
    /// <param name="cancellationToken"></param>
    private async Task<Ok> UnblockWarehouse([FromServices] ICommandExecutor commandExecutor, [FromRoute] int warehouseId, CancellationToken cancellationToken)
    {
        await commandExecutor.Execute(new UnblockWarehouseCommand(warehouseId), cancellationToken);
        return TypedResults.Ok();
    }

    /// <summary>
    /// Blocks a warehouse
    /// </summary>
    /// <param name="commandExecutor"></param>
    /// <param name="warehouseId"></param>
    /// <param name="cancellationToken"></param>
    private async Task<Ok> BlockWarehouse([FromServices] ICommandExecutor commandExecutor, [FromRoute] int warehouseId, CancellationToken cancellationToken)
    {
        await commandExecutor.Execute(new BlockWarehouseCommand(warehouseId), cancellationToken);
        return TypedResults.Ok();
    }

    /// <summary>
    /// Deletes a warehouse
    /// </summary>
    /// <param name="commandExecutor"></param>
    /// <param name="warehouseId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    private async Task<NoContent> DeleteWarehouse([FromServices] ICommandExecutor commandExecutor, [FromRoute] int warehouseId, CancellationToken cancellationToken)
    {
        await commandExecutor.Execute(new DeleteWarehouseCommand(warehouseId), cancellationToken);
        return TypedResults.NoContent();
    }

    /// <summary>
    /// Updates a warehouse
    /// </summary>
    /// <param name="commandExecutor"></param>
    /// <param name="warehouseId"></param>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    private async Task<Ok> UpdateWarehouse([FromServices] ICommandExecutor commandExecutor, [FromRoute] int warehouseId, [FromBody] UpdateWarehouseRequest request, CancellationToken cancellationToken)
    {
        await commandExecutor.Execute(new UpdateWarehouseCommand(warehouseId, request.Code, request.Name) { Description = request.Description }, cancellationToken);
        return TypedResults.Ok();
    }

    /// <summary>
    /// Creates a warehouse
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    private async Task<Ok<IdentityResponse>> CreateWarehouse([FromServices] ICommandExecutor sender, [FromBody] CreateWarehouseRequest request, CancellationToken cancellationToken)
    {
        var warehouse = await sender.Execute(new CreateWarehouseCommand(request.Code, request.Name) { Description = request.Description }, cancellationToken);
        return TypedResults.Ok((IdentityResponse)warehouse);
    }
}

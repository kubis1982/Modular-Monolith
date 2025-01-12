namespace ModularMonolith.Modules.Orders
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Http.HttpResults;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Routing;
    using ModularMonolith.Modules.Orders.Commands.Orders.Purchase;
    using ModularMonolith.Modules.Orders.Commands.Orders.Sale;
    using ModularMonolith.Modules.Orders.Queries.Orders;
    using ModularMonolith.Modules.Orders.Requests.Orders;
    using ModularMonolith.Shared.CQRS.Commands;
    using ModularMonolith.Shared.CQRS.Queries;
    using ModularMonolith.Shared.Modules;
    using ModularMonolith.Shared.Modules.Endpoints.Responses;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public class Endpoints : IModuleEndpoints
    {
        public void AddRoutes(IEndpointRouteBuilder endpointRouteBuilder)
        {
            endpointRouteBuilder.MapPost("purchase-orders", CreatePurchaseOrder);
            endpointRouteBuilder.MapPut("purchase-orders/{orderId}", UpdatePurchaseOrder);
            endpointRouteBuilder.MapDelete("purchase-orders/{orderId}", DeletePurchaseOrder);
            endpointRouteBuilder.MapPatch("purchase-orders/{orderId}/confirm", ConfirmPurchaseOrder);

            endpointRouteBuilder.MapPost("purchase-orders/{orderId:int}/items", CreatePurchaseItem);
            endpointRouteBuilder.MapPut("purchase-orders/{orderId:int}/items/{itemId:int}", UpdatePurchaseItem);
            endpointRouteBuilder.MapDelete("purchase-orders/{orderId:int}/items/{itemId:int}", DeletePurchaseItem);

            endpointRouteBuilder.MapPost("sales-orders", CreateSalesOrder);
            endpointRouteBuilder.MapPut("sales-orders/{orderId}", UpdateSalesOrder);
            endpointRouteBuilder.MapDelete("sales-orders/{orderId}", DeleteSalesOrder);
            endpointRouteBuilder.MapPatch("sales-orders/{orderId}/confirm", ConfirmSalesOrder);

            endpointRouteBuilder.MapPost("sales-orders/{orderId:int}/items", CreateSalesItem);
            endpointRouteBuilder.MapPut("sales-orders/{orderId:int}/items/{itemId:int}", UpdateSalesItem);
            endpointRouteBuilder.MapDelete("sales-orders/{orderId:int}/items/{itemId:int}", DeleteSalesItem);

            endpointRouteBuilder.MapGet("order-states", GetOrderStates);
        }

        /// <summary>
        /// Get all order states
        /// </summary>
        /// <param name="queryExecutor"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        private async Task<Ok<IEnumerable<GetOrderStatesQueryResult>>> GetOrderStates([FromServices] IQueryExecutor queryExecutor, CancellationToken cancellationToken)
        {
            var results = await queryExecutor.Execute(new GetOrderStatesQuery(), cancellationToken);
            return TypedResults.Ok(results);
        }

        /// <summary>
        /// Creates a purchase order
        /// </summary>
        /// <param name="commandExecutor"></param>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        private async Task<Created<IdentityResponse>> CreatePurchaseOrder([FromServices] ICommandExecutor commandExecutor, [FromBody] CreatePurchaseOrderRequest request, CancellationToken cancellationToken)
        {
            var order = await commandExecutor.Execute(new CreatePurchaseOrderCommand
            {
                ContractorId = request.ContractorId,
                WarehouseId = request.WarehouseId,
                ExecutionDate = request.ExecutionDate,
                Description = request.Description,
                AddressLine1 = request.Address?.Line1,
                AddressLine2 = request.Address?.Line2,
                AddressPostalCode = request.Address?.PostalCode,
                AddressCity = request.Address?.City,
                AddressCountry = request.Address?.Country
            }, cancellationToken);
            return TypedResults.Created(this.GetUrl(ModuleDefinition.MODULE_CODE, $"/purchase-orders/{order.Id}"), (IdentityResponse)order);
        }

        /// <summary>
        /// Updates a purchase order
        /// </summary>
        /// <param name="commandExecutor"></param>
        /// <param name="orderId"></param>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        private async Task<Ok> UpdatePurchaseOrder([FromServices] ICommandExecutor commandExecutor, [FromRoute] int orderId, [FromBody] UpdatePurchaseOrderRequest request, CancellationToken cancellationToken)
        {
            await commandExecutor.Execute(new UpdatePurchaseOrderCommand(orderId)
            {
                ContractorId = request.ContractorId,
                WarehouseId = request.WarehouseId,
                ExecutionDate = request.ExecutionDate,
                Description = request.Description,
                AddressLine1 = request.Address?.Line1,
                AddressLine2 = request.Address?.Line2,
                AddressPostalCode = request.Address?.PostalCode,
                AddressCity = request.Address?.City,
                AddressCountry = request.Address?.Country
            }, cancellationToken);
            return TypedResults.Ok();
        }

        /// <summary>
        /// Deletes a purchase order
        /// </summary>
        /// <param name="commandExecutor"></param>
        /// <param name="orderId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        private async Task<NoContent> DeletePurchaseOrder([FromServices] ICommandExecutor commandExecutor, [FromRoute] int orderId, CancellationToken cancellationToken)
        {
            await commandExecutor.Execute(new DeletePurchaseOrderCommand(orderId), cancellationToken);
            return TypedResults.NoContent();
        }

        /// <summary>
        /// Confirm a purchase order
        /// </summary>
        /// <param name="commandExecutor"></param>
        /// <param name="orderId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        private async Task<Ok> ConfirmPurchaseOrder([FromServices] ICommandExecutor commandExecutor, [FromRoute] int orderId, CancellationToken cancellationToken)
        {
            await commandExecutor.Execute(new ConfirmPurchaseOrderCommand(orderId), cancellationToken);
            return TypedResults.Ok();
        }

        /// <summary>
        /// Creates a purchase item
        /// </summary>
        /// <param name="commandExecutor"></param>
        /// <param name="orderId"></param>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        private async Task<Created<IdentityResponse>> CreatePurchaseItem([FromServices] ICommandExecutor commandExecutor, [FromRoute] int orderId, [FromBody] CreatePurchaseItemRequest request, CancellationToken cancellationToken)
        {
            var orderItem = await commandExecutor.Execute(new CreatePurchaseItemCommand(orderId)
            {
                ArticleId = request.ArticleId,
                Quantity = request.Quantity,
                Unit = request.Unit,
                Numerator = request.Numerator,
                Denominator = request.Denominator,
                Description = request.Description
            }, cancellationToken);
            return TypedResults.Created(this.GetUrl(ModuleDefinition.MODULE_CODE, $"purchase-orders/{orderId}/items/{orderItem.Id}"), (IdentityResponse)orderItem);
        }

        /// <summary>
        /// Updates a purchase item
        /// </summary>
        /// <param name="commandExecutor"></param>
        /// <param name="orderId"></param>
        /// <param name="itemId"></param>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        private async Task<Ok> UpdatePurchaseItem([FromServices] ICommandExecutor commandExecutor, [FromRoute] int orderId, [FromRoute] int itemId, [FromBody] UpdatePurchaseItemRequest request, CancellationToken cancellationToken)
        {
            await commandExecutor.Execute(new UpdatePurchaseItemCommand(orderId, itemId)
            {
                ArticleId = request.ArticleId,
                Quantity = request.Quantity,
                Unit = request.Unit,
                Numerator = request.Numerator,
                Denominator = request.Denominator,
                Description = request.Description
            }, cancellationToken);
            return TypedResults.Ok();
        }

        private async Task<NoContent> DeletePurchaseItem([FromServices] ICommandExecutor commandExecutor, [FromRoute] int orderId, [FromRoute] int itemId, CancellationToken cancellationToken)
        {
            await commandExecutor.Execute(new DeletePurchaseItemCommand(orderId, itemId), cancellationToken);
            return TypedResults.NoContent();
        }

        /// <summary>
        /// Creates a sales order
        /// </summary>
        /// <param name="commandExecutor"></param>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        private async Task<Created<IdentityResponse>> CreateSalesOrder([FromServices] ICommandExecutor commandExecutor, [FromBody] CreateSalesOrderRequest request, CancellationToken cancellationToken)
        {
            var order = await commandExecutor.Execute(new CreateSalesOrderCommand
            {
                ContractorId = request.ContractorId,
                WarehouseId = request.WarehouseId,
                ExecutionDate = request.ExecutionDate,
                Description = request.Description,
                AddressLine1 = request.Address?.Line1,
                AddressLine2 = request.Address?.Line2,
                AddressPostalCode = request.Address?.PostalCode,
                AddressCity = request.Address?.City,
                AddressCountry = request.Address?.Country
            }, cancellationToken);
            return TypedResults.Created(this.GetUrl(ModuleDefinition.MODULE_CODE, $"/sales-orders/{order.Id}"), (IdentityResponse)order);
        }

        /// <summary>
        /// Updates a sales order
        /// </summary>
        /// <param name="commandExecutor"></param>
        /// <param name="orderId"></param>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        private async Task<Ok> UpdateSalesOrder([FromServices] ICommandExecutor commandExecutor, [FromRoute] int orderId, [FromBody] UpdateSalesOrderRequest request, CancellationToken cancellationToken)
        {
            await commandExecutor.Execute(new UpdateSalesOrderCommand(orderId)
            {
                ContractorId = request.ContractorId,
                WarehouseId = request.WarehouseId,
                ExecutionDate = request.ExecutionDate,
                Description = request.Description,
                AddressLine1 = request.Address?.Line1,
                AddressLine2 = request.Address?.Line2,
                AddressPostalCode = request.Address?.PostalCode,
                AddressCity = request.Address?.City,
                AddressCountry = request.Address?.Country
            }, cancellationToken);
            return TypedResults.Ok();
        }

        /// <summary>
        /// Deletes a sales order
        /// </summary>
        /// <param name="commandExecutor"></param>
        /// <param name="orderId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        private async Task<NoContent> DeleteSalesOrder([FromServices] ICommandExecutor commandExecutor, [FromRoute] int orderId, CancellationToken cancellationToken)
        {
            await commandExecutor.Execute(new DeleteSalesOrderCommand(orderId), cancellationToken);
            return TypedResults.NoContent();
        }

        /// <summary>
        /// Confirm a sales order
        /// </summary>
        /// <param name="commandExecutor"></param>
        /// <param name="orderId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        private async Task<Ok> ConfirmSalesOrder([FromServices] ICommandExecutor commandExecutor, [FromRoute] int orderId, CancellationToken cancellationToken)
        {
            await commandExecutor.Execute(new ConfirmSalesOrderCommand(orderId), cancellationToken);
            return TypedResults.Ok();
        }

        /// <summary>
        /// Creates a sales item
        /// </summary>
        /// <param name="commandExecutor"></param>
        /// <param name="orderId"></param>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        private async Task<Created<IdentityResponse>> CreateSalesItem([FromServices] ICommandExecutor commandExecutor, [FromRoute] int orderId, [FromBody] CreateSalesItemRequest request, CancellationToken cancellationToken)
        {
            var orderItem = await commandExecutor.Execute(new CreateSalesItemCommand(orderId)
            {
                ArticleId = request.ArticleId,
                Quantity = request.Quantity,
                Unit = request.Unit,
                Numerator = request.Numerator,
                Denominator = request.Denominator,
                Description = request.Description
            }, cancellationToken);
            return TypedResults.Created(this.GetUrl(ModuleDefinition.MODULE_CODE, $"sales-orders/{orderId}/items/{orderItem.Id}"), (IdentityResponse)orderItem);
        }

        /// <summary>
        /// Updates a sales item
        /// </summary>
        /// <param name="commandExecutor"></param>
        /// <param name="orderId"></param>
        /// <param name="itemId"></param>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        private async Task<Ok> UpdateSalesItem([FromServices] ICommandExecutor commandExecutor, [FromRoute] int orderId, [FromRoute] int itemId, [FromBody] UpdateSalesItemRequest request, CancellationToken cancellationToken)
        {
            await commandExecutor.Execute(new UpdateSalesItemCommand(orderId, itemId)
            {
                ArticleId = request.ArticleId,
                Quantity = request.Quantity,
                Unit = request.Unit,
                Numerator = request.Numerator,
                Denominator = request.Denominator,
                Description = request.Description
            }, cancellationToken);
            return TypedResults.Ok();
        }

        /// <summary>
        /// Deletes a sales item
        /// </summary>
        /// <param name="commandExecutor"></param>
        /// <param name="orderId"></param>
        /// <param name="itemId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        private async Task<NoContent> DeleteSalesItem([FromServices] ICommandExecutor commandExecutor, [FromRoute] int orderId, [FromRoute] int itemId, CancellationToken cancellationToken)
        {
            await commandExecutor.Execute(new DeleteSalesItemCommand(orderId, itemId), cancellationToken);
            return TypedResults.NoContent();
        }
    }
}

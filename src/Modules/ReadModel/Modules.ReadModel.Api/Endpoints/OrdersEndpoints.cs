namespace ModularMonolith.Modules.ReadModel.Endpoints
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Http.HttpResults;
    using Microsoft.AspNetCore.Mvc;
    using ModularMonolith.Modules.ReadModel.Persistance.ReadModel.Ordering;
    using ModularMonolith.Modules.ReadModel.Queries.Orders;
    using ModularMonolith.Shared.CQRS.Queries;
    using System.Threading;
    using System.Threading.Tasks;

    internal class OrdersEndpoints() : ModuleEndpoints(Modules.Orders) {
        public override void AddRoutes(IModuleEndpointRouteBuilder endpointRouteBuilder) {
            endpointRouteBuilder.MapGet("sales-orders/{orderId}", GetSalesOrder);
            endpointRouteBuilder.MapGet("sales-orders/{orderId}/items/{orderItemId}", GetSalesOrderItem);
            endpointRouteBuilder.MapGet("purchase-orders/{orderId}", GetPurchaseOrder);
            endpointRouteBuilder.MapGet("purchase-orders/{orderId}/items/{orderItemId}", GetPurchaseOrderItem);
        }       
        private async Task<Results<Ok<Queries.Orders.GetOrderQueryResult>, NotFound>> GetSalesOrder([FromServices] IQueryExecutor queryExecutor, [FromRoute] int orderId, CancellationToken cancellationToken) {
            var result = await queryExecutor.Execute(new Queries.Orders.GetOrderQuery(OrderType.Sales, orderId), cancellationToken);
            return result switch {
                null => TypedResults.NotFound(),
                _ => TypedResults.Ok(result)
            };
        }

        private async Task<Results<Ok<GetOrderItemQueryResult>, NotFound>> GetSalesOrderItem([FromServices] IQueryExecutor queryExecutor, [FromRoute] int orderId, [FromRoute] int orderItemId, CancellationToken cancellationToken) {
            var result = await queryExecutor.Execute(new GetOrderItemQuery(OrderType.Sales, orderId, orderItemId), cancellationToken);
            return result switch {
                null => TypedResults.NotFound(),
                _ => TypedResults.Ok(result)
            };
        }

        private async Task<Results<Ok<GetOrderQueryResult>, NotFound>> GetPurchaseOrder([FromServices] IQueryExecutor queryExecutor, [FromRoute] int orderId, CancellationToken cancellationToken) {
            var result = await queryExecutor.Execute(new Queries.Orders.GetOrderQuery(OrderType.Purchase, orderId), cancellationToken);
            return result switch {
                null => TypedResults.NotFound(),
                _ => TypedResults.Ok(result)
            };
        }

        private async Task<Results<Ok<GetOrderItemQueryResult>, NotFound>> GetPurchaseOrderItem([FromServices] IQueryExecutor queryExecutor, [FromRoute] int orderId, [FromRoute] int orderItemId, CancellationToken cancellationToken) {
            var result = await queryExecutor.Execute(new GetOrderItemQuery(OrderType.Purchase, orderId, orderItemId), cancellationToken);
            return result switch {
                null => TypedResults.NotFound(),
                _ => TypedResults.Ok(result)
            };
        }
    }
}

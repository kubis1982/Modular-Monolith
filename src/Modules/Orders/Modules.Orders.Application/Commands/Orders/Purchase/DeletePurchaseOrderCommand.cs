namespace ModularMonolith.Modules.Orders.Commands.Orders.Purchase
{
    using ModularMonolith.Modules.Orders.Domain.Orders;
    using System.Threading;
    using System.Threading.Tasks;

    public sealed record DeletePurchaseOrderCommand(int OrderId) : UnitOfWorkCommand
    {
        internal class DeletePurchaseOrderCommandHandler(IOrderRepository orderRepository) : UnitOfWorkCommandHandler<DeletePurchaseOrderCommand>
        {
            public override async Task Handle(DeletePurchaseOrderCommand request, CancellationToken cancellationToken)
            {
                Order order = await orderRepository.SingleAsync(OrderSpec.ById(OrderType.Purchase, request.OrderId), cancellationToken);
                order.Remove();
                await orderRepository.DeleteAsync(order, cancellationToken);
            }
        }
    }
}

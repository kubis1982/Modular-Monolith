namespace ModularMonolith.Modules.Ordering.Commands.Orders.Purchase
{
    using ModularMonolith.Modules.Ordering.Domain.Orders;
    using System.Threading;
    using System.Threading.Tasks;

    public sealed record ConfirmPurchaseOrderCommand(int OrderId) : UnitOfWorkCommand
    {
        internal class ConfirmPurchaseOrderCommandHandler(IOrderRepository orderRepository) : UnitOfWorkCommandHandler<ConfirmPurchaseOrderCommand>
        {
            public override async Task Handle(ConfirmPurchaseOrderCommand command, CancellationToken cancellationToken)
            {
                Order order = await orderRepository.SingleAsync(OrderSpec.ByIdWithAnyItem(OrderType.Purchase, command.OrderId), cancellationToken);
                order.Confirm();
            }
        }
    }
}

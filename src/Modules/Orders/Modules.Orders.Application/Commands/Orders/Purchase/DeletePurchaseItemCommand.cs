namespace ModularMonolith.Modules.Orders.Commands.Orders.Purchase
{
    using ModularMonolith.Modules.Orders.Domain.Orders;
    using System.Threading;
    using System.Threading.Tasks;

    public sealed record DeletePurchaseItemCommand(int OrderId, int ItemId) : UnitOfWorkCommand
    {
        internal class DeletePurchaseItemCommandHandler(IOrderRepository orderRepository) : UnitOfWorkCommandHandler<DeletePurchaseItemCommand>
        {
            public override async Task Handle(DeletePurchaseItemCommand command, CancellationToken cancellationToken)
            {
                Order order = await orderRepository.SingleAsync(OrderSpec.ByIdWithItems(OrderType.Purchase, command.OrderId), cancellationToken);
                order.RemoveItem(command.ItemId);
            }
        }
    }
}

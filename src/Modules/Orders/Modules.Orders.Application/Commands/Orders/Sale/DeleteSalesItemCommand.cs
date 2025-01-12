namespace ModularMonolith.Modules.Orders.Commands.Orders.Sale
{
    using ModularMonolith.Modules.Orders.Domain.Orders;
    using System.Threading;
    using System.Threading.Tasks;

    public sealed record DeleteSalesItemCommand(int OrderId, int ItemId) : UnitOfWorkCommand
    {
        internal class DeleteSalesItemCommandHandler(IOrderRepository orderRepository) : UnitOfWorkCommandHandler<DeleteSalesItemCommand>
        {
            public override async Task Handle(DeleteSalesItemCommand command, CancellationToken cancellationToken)
            {
                Order order = await orderRepository.SingleAsync(OrderSpec.ByIdWithItems(OrderType.Sale, command.OrderId), cancellationToken);
                order.RemoveItem(command.ItemId);
            }
        }
    }
}

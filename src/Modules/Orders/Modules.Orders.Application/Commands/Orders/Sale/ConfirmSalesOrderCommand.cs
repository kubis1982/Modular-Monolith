namespace ModularMonolith.Modules.Ordering.Commands.Orders.Sale
{
    using ModularMonolith.Modules.Ordering.Domain.Orders;
    using System.Threading;
    using System.Threading.Tasks;

    public sealed record ConfirmSalesOrderCommand(int OrderId) : UnitOfWorkCommand
    {
        internal class ConfirmSalesOrderCommandHandler(IOrderRepository orderRepository) : UnitOfWorkCommandHandler<ConfirmSalesOrderCommand>
        {
            public override async Task Handle(ConfirmSalesOrderCommand command, CancellationToken cancellationToken)
            {
                Order order = await orderRepository.SingleAsync(OrderSpec.ByIdWithAnyItem(OrderType.Sale, command.OrderId), cancellationToken);
                order.Confirm();
            }
        }
    }
}

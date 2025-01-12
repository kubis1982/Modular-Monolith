namespace ModularMonolith.Modules.Orders.Commands.Orders.Sale
{
    using ModularMonolith.Modules.Orders.Domain.Orders;
    using System.Threading;
    using System.Threading.Tasks;

    public sealed record DeleteSalesOrderCommand(int OrderId) : UnitOfWorkCommand
    {
        internal class DeleteSalesOrderCommandHandler(IOrderRepository orderRepository) : UnitOfWorkCommandHandler<DeleteSalesOrderCommand>
        {
            public override async Task Handle(DeleteSalesOrderCommand command, CancellationToken cancellationToken)
            {
                Order order = await orderRepository.SingleAsync(OrderSpec.ById(OrderType.Sale, command.OrderId), cancellationToken);
                order.Remove();
                await orderRepository.DeleteAsync(order, cancellationToken);
            }
        }
    }
}

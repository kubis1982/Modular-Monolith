namespace ModularMonolith.Modules.Orders.Persistance.WriteModel.Repositories
{
    using ModularMonolith.Modules.Orders.Domain.Orders;

    internal class OrderRepository(WriteDbContext dbContext) : Repository<Order, OrderSpec>(dbContext), IOrderRepository
    {
    }
}

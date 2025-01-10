namespace ModularMonolith.Modules.Ordering.Persistance.WriteModel.Repositories
{
    using ModularMonolith.Modules.Ordering.Domain.Orders;

    internal class OrderRepository(WriteDbContext dbContext) : Repository<Order, OrderSpec>(dbContext), IOrderRepository
    {
    }
}

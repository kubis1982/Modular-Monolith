namespace ModularMonolith.Modules.Orders.Domain.Orders
{
    using System;
    using System.Data;
    using System.Linq;

    /// <summary>
    /// Represents a specification for querying orders.
    /// </summary>
    public sealed class OrderSpec : Specification<Order>, ISingleResultSpecification<Order>
    {
        private OrderSpec(Action<ISpecificationBuilder<Order>> action)
            => action(Query);

        public static OrderSpec ById(OrderType orderType, OrderId orderId)
            => new(n => n.Where(n => n.OrderType == orderType && n.Id == orderId));

        public static OrderSpec ByIdWithItems(OrderType orderType, OrderId orderId)
            => new(n => n.Where(n => n.OrderType == orderType && n.Id == orderId).Include(n => n.Items));

        public static OrderSpec ByIdWithItem(OrderType orderType, OrderId orderId, OrderItemId itemId)
            => new(n => n.Where(n => n.OrderType == orderType && n.Id == orderId).Include(n => n.Items.Where(m => m.Id == itemId)));

        public static OrderSpec ByIdWithAnyItem(OrderType orderType, OrderId orderId)
            => new(n => n.Where(n => n.OrderType == orderType && n.Id == orderId).Include(n => n.Items.Take(1)));

        public static OrderSpec ByIdWithLastPosition(OrderType orderType, OrderId orderId)
            => new(n => n.Where(n => n.OrderType == orderType && n.Id == orderId).Include(n => n.Items.OrderByDescending(n => n.Position).Take(1)));
    }
}

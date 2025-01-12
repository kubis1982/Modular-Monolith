namespace ModularMonolith.Modules.Orders.Domain.Orders.Events
{
    public sealed record OrderItemRemovedEvent : OrderDomainEvent
    {
        public OrderItemRemovedEvent(Order order, OrderItemId orderItemId) : base(order)
        {
            OrderItemId = orderItemId;
        }

        public OrderItemId OrderItemId { get; }
    }
}
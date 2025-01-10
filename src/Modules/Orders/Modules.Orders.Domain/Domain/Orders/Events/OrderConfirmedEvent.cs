namespace ModularMonolith.Modules.Ordering.Domain.Orders.Events
{
    public sealed record OrderConfirmedEvent : OrderDomainEvent
    {
        public OrderConfirmedEvent(Order entity) : base(entity)
        {
        }
    }
}
namespace ModularMonolith.Modules.Orders.Domain.Orders.Events
{
    public sealed record OrderConfirmedEvent : OrderDomainEvent
    {
        public OrderConfirmedEvent(Order entity) : base(entity)
        {
        }
    }
}
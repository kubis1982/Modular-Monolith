namespace ModularMonolith.Modules.Ordering.Domain.Orders.Events
{
    internal record OrderCompletedEvent : OrderDomainEvent
    {
        public OrderCompletedEvent(Order entity) : base(entity)
        {
        }
    }
}
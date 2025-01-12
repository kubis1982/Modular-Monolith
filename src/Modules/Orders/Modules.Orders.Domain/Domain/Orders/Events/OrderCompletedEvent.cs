namespace ModularMonolith.Modules.Orders.Domain.Orders.Events
{
    internal record OrderCompletedEvent : OrderDomainEvent
    {
        public OrderCompletedEvent(Order entity) : base(entity)
        {
        }
    }
}
namespace ModularMonolith.Modules.Ordering.Domain.Orders.Events
{
    internal record OrderInProgressEvent : OrderDomainEvent
    {
        public OrderInProgressEvent(Order entity) : base(entity)
        {
        }
    }
}
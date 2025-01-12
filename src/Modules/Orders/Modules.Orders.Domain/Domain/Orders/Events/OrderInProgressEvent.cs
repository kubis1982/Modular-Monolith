namespace ModularMonolith.Modules.Orders.Domain.Orders.Events
{
    internal record OrderInProgressEvent : OrderDomainEvent
    {
        public OrderInProgressEvent(Order entity) : base(entity)
        {
        }
    }
}
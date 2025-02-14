﻿namespace ModularMonolith.Modules.Ordering.Domain.Orders.Events
{
    public sealed record OrderRemovedEvent : OrderDomainEvent
    {
        public OrderRemovedEvent(Order entity) : base(entity)
        {
        }
    }
}

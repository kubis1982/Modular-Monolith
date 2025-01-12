namespace ModularMonolith.Modules.Orders.Domain.Orders.Tests
{
    using System;

    public abstract class OrderFactory
    {
        public abstract Orders.Order Create(Contractor contractor, Warehouse warehouse, DateTime dateTime, string orderNo, string? description, Address address);
    }
}

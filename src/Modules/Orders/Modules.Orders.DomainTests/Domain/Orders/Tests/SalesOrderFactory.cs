namespace ModularMonolith.Modules.Ordering.Domain.Orders.Tests
{
    using ModularMonolith.Modules.Ordering.Domain;
    using System;

    internal class SalesOrderFactory : OrderFactory
    {
        public override Orders.Order Create(Contractor contractor, Warehouse warehouse, DateTime dateTime, string orderNo, string? description, Address address) => Orders.SalesOrder.Create(contractor, warehouse, dateTime, orderNo, description, address);
    }
}

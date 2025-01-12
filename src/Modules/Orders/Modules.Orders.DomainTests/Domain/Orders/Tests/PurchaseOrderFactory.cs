namespace ModularMonolith.Modules.Orders.Domain.Orders.Tests
{
    using ModularMonolith.Modules.Orders.Domain;
    using System;

    internal class PurchaseOrderFactory : OrderFactory
    {
        public override Orders.Order Create(Contractor contractor, Warehouse warehouse, DateTime dateTime, string orderNo, string? description, Address address) => Orders.PurchaseOrder.Create(contractor, warehouse, dateTime, orderNo, description, address);
    }
}

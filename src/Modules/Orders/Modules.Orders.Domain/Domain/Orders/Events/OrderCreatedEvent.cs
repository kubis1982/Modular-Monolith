namespace ModularMonolith.Modules.Ordering.Domain.Orders.Events
{
    using System;

    public sealed record OrderCreatedEvent : OrderDomainEvent
    {
        public OrderCreatedEvent(Order entity, ContractorId contractorId, WarehouseId warehouseId, DateTime executionDate, OrderStatus state, OrderNumber orderNo, string? description, Address address) : base(entity)
        {
            ContractorId = contractorId;
            WarehouseId = warehouseId;
            ExecutionDate = executionDate;
            State = state;
            OrderNo = orderNo;
            Description = description;
            Address = address;
        }

        public ContractorId ContractorId { get; }
        public WarehouseId WarehouseId { get; }
        public DateTime ExecutionDate { get; }
        public OrderStatus State { get; }
        public OrderNumber OrderNo { get; }
        public string? Description { get; }
        public Address Address { get; }
    }
}

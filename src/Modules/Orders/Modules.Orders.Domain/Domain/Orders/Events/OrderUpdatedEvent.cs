namespace ModularMonolith.Modules.Orders.Domain.Orders.Events
{
    using System;

    public sealed record OrderUpdatedEvent : OrderDomainEvent
    {
        public OrderUpdatedEvent(Order entity, ContractorId contractorId, WarehouseId warehouseId, DateTime executionDate, string? description, Address address) : base(entity)
        {
            ContractorId = contractorId;
            WarehouseId = warehouseId;
            ExecutionDate = executionDate;
            Description = description;
            Address = address;
        }

        public ContractorId ContractorId { get; }
        public WarehouseId WarehouseId { get; }
        public DateTime ExecutionDate { get; set; }
        public string? Description { get; }
        public Address Address { get; }
    }
}

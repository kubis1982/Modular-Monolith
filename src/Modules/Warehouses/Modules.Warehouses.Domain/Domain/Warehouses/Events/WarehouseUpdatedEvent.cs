namespace ModularMonolith.Modules.Warehouses.Domain.Warehouses.Events
{
    public sealed record WarehouseUpdatedEvent : WarehouseDomainEvent
    {
        public WarehouseUpdatedEvent(Warehouse Warehouse, WarehouseCode Code, WarehouseName Name, string? description) : base(Warehouse)
        {
            this.Code = Code;
            this.Name = Name;
            Description = description;
        }

        public WarehouseName Name { get; }
        public WarehouseCode Code { get; }
        public string? Description { get; }
    }


}

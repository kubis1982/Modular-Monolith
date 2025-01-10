namespace ModularMonolith.Modules.Warehouses.Domain.Warehouses.Events
{
    public sealed record WarehouseCreatedEvent : WarehouseDomainEvent
    {
        public WarehouseCreatedEvent(Warehouse Warehouse, WarehouseCode Code, WarehouseName Name, string? description) : base(Warehouse)
        {
            this.Name = Name;
            this.Code = Code;
            Description = description;
        }

        public WarehouseName Name { get; }
        public WarehouseCode Code { get; }
        public string? Description { get; }
    }


}

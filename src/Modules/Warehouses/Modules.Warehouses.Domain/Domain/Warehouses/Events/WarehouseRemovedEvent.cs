namespace ModularMonolith.Modules.Warehouses.Domain.Warehouses.Events
{
    public record WarehouseRemovedEvent : WarehouseDomainEvent
    {
        public WarehouseRemovedEvent(Warehouse warehouse) : base(warehouse)
        {
        }
    }
}

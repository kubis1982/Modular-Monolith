namespace ModularMonolith.Modules.Warehouses.Domain.Warehouses.Events
{
    public record WarehouseBlockedEvent : WarehouseDomainEvent
    {
        public WarehouseBlockedEvent(Warehouse warehouse) : base(warehouse)
        {
        }
    }
}

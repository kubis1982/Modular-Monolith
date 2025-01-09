namespace ModularMonolith.Modules.Warehouses.Domain.Warehouses.Events
{
    public record WarehouseUnblockedEvent : WarehouseDomainEvent
    {
        public WarehouseUnblockedEvent(Warehouse warehouse) : base(warehouse)
        {
        }
    }
}

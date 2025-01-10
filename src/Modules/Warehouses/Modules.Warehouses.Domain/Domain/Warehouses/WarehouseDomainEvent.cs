namespace ModularMonolith.Modules.Warehouses.Domain.Warehouses
{
    public abstract record WarehouseDomainEvent : IDomainEvent
    {
        private readonly Warehouse warehouse;

        public WarehouseId WarehouseId => warehouse.Id;

        public WarehouseDomainEvent(Warehouse warehouse)
        {
            this.warehouse = warehouse;
        }
    }
}

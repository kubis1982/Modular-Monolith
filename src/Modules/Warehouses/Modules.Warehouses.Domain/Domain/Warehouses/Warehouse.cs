namespace ModularMonolith.Modules.Warehouses.Domain.Warehouses
{
    public class Warehouse : DomainEntity<WarehouseId, int, EntityType>, IAggregateRoot
    {
        internal WarehouseCode Code { get; private set; }
        internal WarehouseName Name { get; private set; }
        internal string? Description { get; private set; }
        internal bool IsBlocked { get; private set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        private Warehouse()
        {
        }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        private Warehouse(WarehouseCode code, WarehouseName name, string? description) : this()
        {
            this.Code = code;
            this.Name = name;
            this.Description = description;
            AddEvent(new WarehouseCreatedEvent(this, code, name, description));
        }
        public void Update(WarehouseCode code, WarehouseName name, string? description)
        {
            this.Code = code;
            this.Name = name;
            this.Description = description;
            AddEvent(new WarehouseUpdatedEvent(this, code, name, description));
        }

        public void Remove(IWarehouseUsageService warehouseUsageService)
        {
            if (warehouseUsageService.IsUsed(Id, out var entityName))
            {
                throw new RemovingUsedWarehouseException(entityName);
            }
            AddEvent(new WarehouseRemovedEvent(this));
        }

        public void Unblock()
        {
            if (IsBlocked == true)
            {
                IsBlocked = false;
                AddEvent(new WarehouseUnblockedEvent(this));
            }
        }

        public void Block()
        {
            if (IsBlocked == false)
            {
                IsBlocked = true;
                AddEvent(new WarehouseBlockedEvent(this));
            }
        }

        public static Warehouse Create(WarehouseCode code, WarehouseName name, string? description)
        {
            return new Warehouse(code, name, description);
        }
    }
}

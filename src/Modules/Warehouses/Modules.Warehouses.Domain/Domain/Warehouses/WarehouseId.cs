namespace ModularMonolith.Modules.Warehouses.Domain.Warehouses
{

    public record WarehouseId : EntityId<int, EntityType>
    {
        public WarehouseId() : this(0)
        {
        }

        public WarehouseId(int id) : base(EntityType.Warehouse, id)
        {
        }

        public static WarehouseId Default => new(1);

        public static implicit operator WarehouseId(int value) => new(value);
        public static implicit operator int(WarehouseId value) => value.Id;
    }
}

namespace ModularMonolith.Modules.Warehouses.Domain.Warehouses.Exceptions
{
    using ModularMonolith.Shared.Exceptions;

    public class WarehouseNotFoundException : EntityNotFoundException
    {
        public WarehouseNotFoundException(int warehouseId) : base($"Magazyn: {warehouseId}")
        {
        }
    }
}

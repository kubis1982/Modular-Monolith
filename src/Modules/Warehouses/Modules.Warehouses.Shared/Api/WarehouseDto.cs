namespace ModularMonolith.Modules.Warehouses.Api
{
    public class WarehouseDto
    {
        public required string TypeId { get; set; }
        public int Id { get; init; }
        public required string Code { get; init; }
        public required string Name { get; init; }
        public bool IsBlocked { get; init; }
    }
}

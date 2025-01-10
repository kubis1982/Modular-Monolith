namespace ModularMonolith.Modules.Warehouses.Api
{
    public class LocationDto
    {
        public required string TypeId { get; set; }
        public int Id { get; init; }
        public required string Path { get; init; }
    }
}

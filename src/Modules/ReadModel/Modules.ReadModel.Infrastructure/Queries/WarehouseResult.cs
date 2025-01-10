namespace ModularMonolith.Modules.ReadModel.Queries {
    public record WarehouseResult : EntityResult {
        public required bool HasLocations { get; init; }
    }
}

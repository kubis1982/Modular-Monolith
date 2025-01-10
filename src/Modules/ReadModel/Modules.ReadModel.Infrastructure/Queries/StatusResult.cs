namespace ModularMonolith.Modules.ReadModel.Queries {
    public record StatusResult {
        public required int Id { get; init; }
        public required string Name { get; init; }
    }
}

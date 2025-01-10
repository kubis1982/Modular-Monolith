namespace ModularMonolith.Modules.ReadModel.Queries {
    public record EntityIdResult {
        public required string TypeId { get; set; }
        public required int Id { get; set; }
    }
}

namespace ModularMonolith.Modules.ReadModel.Queries {
    public record OrderResult {
        public required string TypeId { get; init; }
        public required int Id { get; init; }
        public required string Number { get; init; }
    }
}

namespace ModularMonolith.Modules.ReadModel.Queries {
    public record DocumentResult {
        public required string TypeId { get; set; }
        public required int Id { get; set; }
        public required string? Number { get; set; }
    }
}

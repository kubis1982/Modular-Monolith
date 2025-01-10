namespace ModularMonolith.Modules.ReadModel.Queries {
    public record EntityCodeResult : EntityIdResult {
        public required string Code { get; set; }
    }
}

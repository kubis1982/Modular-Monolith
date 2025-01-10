namespace ModularMonolith.Modules.ReadModel.Queries {
    public record EntityResult : EntityCodeResult {
        public required string Name { get; set; }
    }
}

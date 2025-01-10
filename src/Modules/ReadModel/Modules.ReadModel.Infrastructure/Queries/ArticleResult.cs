namespace ModularMonolith.Modules.ReadModel.Queries {
    public record ArticleResult : EntityResult {
        public required string Unit { get; init; }
    }
}

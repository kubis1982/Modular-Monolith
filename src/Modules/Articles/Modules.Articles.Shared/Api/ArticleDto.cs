namespace ModularMonolith.Modules.Articles.Api
{
    public class ArticleDto
    {
        public required string TypeId { get; set; }
        public int Id { get; init; }
        public required string Code { get; init; }
        public required string Name { get; init; }
        public required string Unit { get; init; }
        public bool IsBlocked { get; init; }
    }
}

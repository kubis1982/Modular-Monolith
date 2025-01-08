namespace ModularMonolith.Modules.Articles.Domain.Articles
{
    public sealed record ArticleUnitId : EntityId<int, EntityType>
    {
        public ArticleUnitId() : this(0)
        {
        }

        public ArticleUnitId(int id) : base(EntityType.ArticleUnit, id)
        {
        }

        public static implicit operator ArticleUnitId(int value) => new(value);
    }
}
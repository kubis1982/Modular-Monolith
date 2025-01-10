namespace ModularMonolith.Modules.Articles.Domain.Articles
{

    public record ArticleId : EntityId<int, EntityType>
    {
        public ArticleId() : this(0)
        {

        }

        public ArticleId(int id) : base(EntityType.Article, id)
        {
        }

        public static implicit operator ArticleId(int value) => new(value);
        public static implicit operator int(ArticleId value) => value.Id;
    }
}

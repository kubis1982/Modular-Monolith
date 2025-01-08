namespace ModularMonolith.Modules.Articles.Domain.Articles
{
    public abstract record ArticleDomainEvent : IDomainEvent
    {
        private readonly Article entity;

        public ArticleDomainEvent(Article entity)
        {
            this.entity = entity;
        }
        public ArticleId ArticleId => entity.Id;
    }
}
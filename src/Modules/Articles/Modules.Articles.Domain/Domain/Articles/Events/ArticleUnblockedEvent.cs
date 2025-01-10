namespace ModularMonolith.Modules.Articles.Domain.Articles.Events
{
    public record ArticleUnblockedEvent : ArticleDomainEvent
    {
        public ArticleUnblockedEvent(Article entity) : base(entity)
        {
        }
    }
}
namespace ModularMonolith.Modules.Articles.Domain.Articles.Events
{
    public record ArticleBlockedEvent : ArticleDomainEvent
    {
        public ArticleBlockedEvent(Article entity) : base(entity)
        {

        }
    }
}
namespace ModularMonolith.Modules.Articles.Domain.Articles.Events
{
    public record ArticleRemovedEvent : ArticleDomainEvent
    {
        public ArticleRemovedEvent(Article entity) : base(entity)
        {
        }
    }
}

namespace ModularMonolith.Modules.Articles.Domain.Articles.Events
{
    using ModularMonolith.Modules.Articles.Domain.MeasurementUnits;

    public record ArticleUpdatedEvent : ArticleDomainEvent
    {
        public ArticleUpdatedEvent(Article article, ArticleName name, ArticleCode code, MeasurementUnitCode unit, string? description) : base(article)
        {
            Name = name;
            Code = code;
            Unit = unit;
            Description = description;
        }

        public ArticleName Name { get; }
        public ArticleCode Code { get; }
        public MeasurementUnitCode Unit { get; }
        public string? Description { get; }
    }
}
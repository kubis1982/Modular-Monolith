namespace ModularMonolith.Modules.Articles.Domain.Articles
{
    using ModularMonolith.Modules.Articles.Domain.Articles.Events;
    using ModularMonolith.Modules.Articles.Domain.Articles.Exceptions;
    using ModularMonolith.Modules.Articles.Domain.MeasurementUnits;

    public partial class Article : DomainEntity<ArticleId, int, EntityType>, IAggregateRoot
    {
        internal ArticleName Name { get; private set; }
        internal ArticleCode Code { get; private set; }
        internal MeasurementUnitCode Unit { get; private set; }
        internal string? Description { get; private set; }
        internal bool IsBlocked { get; private set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        private Article()
        {
        }

#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        private Article(ArticleName name, ArticleCode code, MeasurementUnitCode unit, string? description) : this()
        {
            Name = name;
            Code = code;
            Unit = unit;
            Description = description ?? string.Empty;
            AddEvent(new ArticleCreatedEvent(this, name, code, unit, description));
        }

        public static Article Create(ArticleName name, ArticleCode code, MeasurementUnitCode unit, string? description)
        {
            return new Article(name, code, unit, description);
        }

        /// <summary>
        /// Update article details.
        /// </summary>
        /// <param name="articleUsageService"></param>
        /// <param name="code"></param>
        /// <param name="name"></param>
        /// <param name="unit"></param>
        /// <param name="description"></param>
        /// <exception cref="RemovingUsedArticleException"></exception>
        public void Update(IArticleUsageService articleUsageService, ArticleCode code, ArticleName name, MeasurementUnitCode unit, string? description)
        {
            if (Unit != unit)
            {
                if (articleUsageService.IsUsed(Id, out string entityName))
                {
                    throw new UpdatingUsedArticleException(entityName);
                }
            }
            Name = name;
            Code = code;
            Unit = unit;
            Description = description ?? string.Empty;
            AddEvent(new ArticleUpdatedEvent(this, name, code, unit, description));
        }

        /// <summary>
        /// Remove article from the system.
        /// </summary>
        /// <param name="articleUsageService"></param>
        /// <exception cref="RemovingUsedArticleException"></exception>
        public void Remove(IArticleUsageService articleUsageService)
        {
            if (articleUsageService.IsUsed(Id, out string entityName))
            {
                throw new RemovingUsedArticleException(entityName);
            }
            AddEvent(new ArticleRemovedEvent(this));
        }

        public void Block()
        {
            if (IsBlocked == false)
            {
                IsBlocked = true;
                AddEvent(new ArticleBlockedEvent(this));
            }
        }

        public void Unblock()
        {
            if (IsBlocked == true)
            {
                IsBlocked = false;
                AddEvent(new ArticleUnblockedEvent(this));
            }
        }
    }
}

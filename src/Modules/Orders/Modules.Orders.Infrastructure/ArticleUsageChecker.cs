namespace ModularMonolith.Modules.Ordering
{
    using ModularMonolith.Modules.Articles;
    using ModularMonolith.Modules.Ordering.Persistance.ReadModel;
    using System.ComponentModel;
    using System.Linq;

    /// <summary>
    /// Article usage checker
    /// </summary>
    internal class ArticleUsageChecker(ReadDbContext readDbContext) : IArticleUsageChecker
    {
        /// <summary>
        /// Check if an article is used in any other entity
        /// </summary>
        /// <param name="articleId"></param>
        /// <param name="entityName"></param>
        /// <returns></returns>
        public bool IsUsed(int articleId, out string entityName)
        {
            if (readDbContext.OrderItems.Any(n => n.ArticleId == articleId))
            {
                entityName = "Pozycja zamówienia";
                return true;
            }
            entityName = string.Empty;
            return false;
        }

        private enum EntityType
        {
            [Description("Dostawa")]
            DeliveryItem = 1,
            [Description("Zamówienie")]
            OrderItem = 2
        }
    }
}

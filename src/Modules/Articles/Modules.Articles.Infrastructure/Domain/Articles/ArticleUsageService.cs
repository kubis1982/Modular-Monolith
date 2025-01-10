namespace ModularMonolith.Modules.Articles.Domain.Articles
{
    using System.Collections.Generic;

    /// <summary>
    /// Service for checking if an article is used in any other entity
    /// </summary>
    /// <param name="articleUsageCheckers"></param>
    internal class ArticleUsageService(IEnumerable<IArticleUsageChecker> articleUsageCheckers) : IArticleUsageService
    {
        /// <summary>
        /// Check if an article is used in any other entity
        /// </summary>
        /// <param name="articleId"></param>
        /// <param name="entityName"></param>
        /// <returns></returns>
        public bool IsUsed(int articleId, out string entityName)
        {
            foreach (IArticleUsageChecker checker in articleUsageCheckers)
            {
                if (checker.IsUsed(articleId, out entityName))
                {
                    return true;
                }
            }
            entityName = string.Empty;
            return false;
        }
    }
}

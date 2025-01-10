namespace ModularMonolith.Modules.Articles.Domain.Articles
{
    /// <summary>
    /// Service for checking if an article is used in any other entity
    /// </summary>
    public interface IArticleUsageService
    {
        /// <summary>
        /// Check if an article is used in any other entity
        /// </summary>
        /// <param name="articleId"></param>
        /// <param name="entityName"></param>
        /// <returns></returns>
        bool IsUsed(int articleId, out string entityName);
    }
}

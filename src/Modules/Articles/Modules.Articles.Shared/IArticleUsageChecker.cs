namespace ModularMonolith.Modules.Articles
{
    /// <summary>
    /// Interface for checking if an article is used in the system.
    /// </summary>
    public interface IArticleUsageChecker
    {
        /// <summary>
        /// Checks if the article is used in the system.
        /// </summary>
        /// <param name="articleId"></param>
        /// <param name="entityName"></param>
        /// <returns></returns>
        bool IsUsed(int articleId, out string entityName);
    }
}

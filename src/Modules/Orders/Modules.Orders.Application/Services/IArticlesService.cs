namespace ModularMonolith.Modules.Ordering.Services
{
    using ModularMonolith.Modules.Ordering.Domain;

    public interface IArticlesService
    {
        public Task<Article> GetArticleAsync(int articleId, CancellationToken cancellationToken);
    }
}

namespace ModularMonolith.Modules.Orders.Services
{
    using ModularMonolith.Modules.Orders.Domain;

    public interface IArticlesService
    {
        public Task<Article> GetArticleAsync(int articleId, CancellationToken cancellationToken);
    }
}

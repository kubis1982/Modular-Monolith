namespace ModularMonolith.Modules.Articles.Api
{
    using System.Threading;
    using System.Threading.Tasks;

    public interface IArticlesModuleApi
    {
        Task<ArticleDto> GetArticleAsync(int articleId, CancellationToken cancellationToken);
    }
}

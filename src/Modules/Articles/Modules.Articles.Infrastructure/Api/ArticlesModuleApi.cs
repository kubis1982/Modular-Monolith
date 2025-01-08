namespace ModularMonolith.Modules.Articles.Api
{
    using ModularMonolith.Modules.Articles.Api.Queries;
    using ModularMonolith.Modules.Articles.Domain.Articles.Exceptions;
    using ModularMonolith.Shared.CQRS.Queries;
    using System.Threading;
    using System.Threading.Tasks;

    internal class ArticlesModuleApi(IQueryExecutor queryExecutor) : IArticlesModuleApi
    {
        public async Task<ArticleDto> GetArticleAsync(int articleId, CancellationToken cancellationToken)
        {
            var article = await queryExecutor.Execute(new GetArticleQuery(articleId), cancellationToken);
            return article ?? throw new ArticleNotFoundException(articleId);
        }
    }
}

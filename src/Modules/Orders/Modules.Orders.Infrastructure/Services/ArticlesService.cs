namespace ModularMonolith.Modules.Ordering.Services
{
    using AutoMapper;
    using ModularMonolith.Modules.Articles.Api;
    using System.Threading;
    using System.Threading.Tasks;

    internal class ArticlesService(IArticlesModuleApi articlesModuleApi, IMapper mapper) : IArticlesService
    {
        public async Task<Article> GetArticleAsync(int articleId, CancellationToken cancellationToken)
        {
            return mapper.Map<Article>(await articlesModuleApi.GetArticleAsync(articleId, cancellationToken));
        }
    }
}

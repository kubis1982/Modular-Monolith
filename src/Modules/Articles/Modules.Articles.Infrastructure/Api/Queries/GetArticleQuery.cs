namespace ModularMonolith.Modules.Articles.Api.Queries
{

    using Microsoft.EntityFrameworkCore;
    using ModularMonolith.Modules.Articles.Api;
    using ModularMonolith.Modules.Articles.Persistance.ReadModel;
    using ModularMonolith.Shared.CQRS.Queries;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Gets articles
    /// </summary>
    internal sealed record GetArticleQuery(int ArticleId) : Query<ArticleDto?>
    {
        internal class GetArticleQueryHandler(ReadDbContext dbContext) : QueryHandler<GetArticleQuery, ArticleDto?>
        {
            public override async Task<ArticleDto?> Handle(GetArticleQuery request, CancellationToken cancellationToken)
            {
                return await dbContext.Articles.Where(n => n.Id == request.ArticleId).Select(n => new ArticleDto
                {
                    TypeId = n.TypeId,
                    Id = n.Id,
                    Code = n.Code,
                    Name = n.Name,
                    Unit = n.Unit,
                    IsBlocked = n.IsBlocked
                }).SingleOrDefaultAsync(cancellationToken: cancellationToken);
            }
        }
    }
}

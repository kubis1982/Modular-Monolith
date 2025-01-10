namespace ModularMonolith.Modules.ReadModel.Queries.Articles {
    using Microsoft.EntityFrameworkCore;
    using ModularMonolith.Modules.ReadModel.Persistance.ReadModel;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    public sealed record GetArticleQuery(int ArticleId) : Query<GetArticleQueryResult?> {
        internal class GetArticleQueryHandler(ReadDbContext dbContext) : QueryHandler<GetArticleQuery, GetArticleQueryResult?> {
            public override async Task<GetArticleQueryResult?> Handle(GetArticleQuery query, CancellationToken cancellationToken) {
                var result = (
                    from articles in dbContext.Articles.Where(n => n.Id == query.ArticleId)
                    select new GetArticleQueryResult {
                        TypeId = articles.TypeId,
                        Id = articles.Id,
                        Code = articles.Code,
                        Name = articles.Name,
                        Unit = articles.Unit,
                        Description = articles.Description,
                        IsBlocked = articles.IsBlocked
                    }
                );
                return await result.SingleOrDefaultAsync(cancellationToken);
            }
        }
    }

    public class GetArticleQueryResult {
        public required string TypeId { get; set; }
        public required int Id { get; set; }
        public required string Code { get; set; }
        public required string Name { get; set; }
        public required string Unit { get; set; }
        public required string? Description { get; set; }
        public required bool IsBlocked { get; set; }
    }
}
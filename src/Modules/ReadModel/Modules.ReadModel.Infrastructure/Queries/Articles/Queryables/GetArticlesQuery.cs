namespace ModularMonolith.Modules.ReadModel.Queries.Articles.Queryables {
    using ModularMonolith.Modules.ReadModel.Persistance.ReadModel;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Gets articles
    /// </summary>
    public sealed record GetArticlesQuery : Query<IQueryable<GetArticlesQueryResult>> {
        internal class GetArticlesQueryHandler(ReadDbContext dbContext) : QueryHandler<GetArticlesQuery, IQueryable<GetArticlesQueryResult>> {
            public override Task<IQueryable<GetArticlesQueryResult>> Handle(GetArticlesQuery request, CancellationToken cancellationToken) {
                return Task.FromResult(dbContext.Articles.Select(n => new GetArticlesQueryResult {
                    Id = n.Id,
                    Code = n.Code,
                    Name = n.Name,
                    Unit = n.Unit,
                    IsBlocked = n.IsBlocked,
                    Description = n.Description
                }).AsQueryable());
            }
        }
    }

    public class GetArticlesQueryResult {
        public int Id { get; set; }
        public required string Code { get; set; }
        public required string Name { get; set; }
        public required string Unit { get; set; }
        public string? Description { get; set; }
        public bool IsBlocked { get; set; }
    }
}
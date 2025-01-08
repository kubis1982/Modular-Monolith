namespace ModularMonolith.Modules.Articles.Queries.Articles
{

    using ModularMonolith.Modules.Articles.Persistance.ReadModel;
    using ModularMonolith.Shared.CQRS.Queries;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Gets articles
    /// </summary>
    public sealed record GetArticlesQuery : Query<IEnumerable<GetArticlesQueryResult>>
    {
        public Expression<Func<ArticleEntity, bool>> Expression { get; }

        private GetArticlesQuery(Expression<Func<ArticleEntity, bool>> expression)
        {
            Expression = expression;
        }

        /// <summary>
        /// Gets all articles
        /// </summary>
        public GetArticlesQuery(bool includeBlocked) : this(n => n.IsBlocked == (includeBlocked ? n.IsBlocked : false)) { }

        /// <summary>
        /// Gets an article by id
        /// </summary>
        /// <param name="articleId"></param>
        public GetArticlesQuery(int articleId) : this(n => n.Id == articleId) { }

        /// <summary>
        /// Gets an article by name
        /// </summary>
        /// <param name="code"></param>
        public GetArticlesQuery(string code) : this(n => n.Code == code) { }

        internal class GetArticlesQueryHandler(ReadDbContext dbContext) : QueryHandler<GetArticlesQuery, IEnumerable<GetArticlesQueryResult>>
        {
            private readonly ReadDbContext dbContext = dbContext;

            public override async Task<IEnumerable<GetArticlesQueryResult>> Handle(GetArticlesQuery request, CancellationToken cancellationToken)
            {
                return await dbContext.Articles.Where(request.Expression).Select(n => new GetArticlesQueryResult
                {
                    Id = n.Id,
                    Code = n.Code,
                    Name = n.Name,
                    Unit = n.Unit,
                    IsBlocked = n.IsBlocked,
                    Description = n.Description
                }).OrderBy(n => n.Name).ToArrayAsync(cancellationToken: cancellationToken);
            }
        }
    }

    public class GetArticlesQueryResult
    {
        public int Id { get; set; }
        public required string Code { get; set; }
        public required string Name { get; set; }
        public required string Unit { get; set; }
        public string? Description { get; set; }
        public bool IsBlocked { get; set; }
    }
}

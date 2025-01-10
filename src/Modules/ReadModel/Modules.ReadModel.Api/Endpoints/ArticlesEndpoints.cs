namespace ModularMonolith.Modules.ReadModel.Endpoints
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Http.HttpResults;
    using Microsoft.AspNetCore.Mvc;
    using ModularMonolith.Modules.ReadModel.Queries.Articles;
    using ModularMonolith.Shared.CQRS.Queries;
    using System.Threading;
    using System.Threading.Tasks;

    internal class ArticlesEndpoints() : ModuleEndpoints(Modules.Articles) {
        public override void AddRoutes(IModuleEndpointRouteBuilder endpointRouteBuilder) {
            endpointRouteBuilder.MapGet("/articles/{articleId}", GetArticle);
        }

        private async Task<Results<Ok<GetArticleQueryResult>, NotFound>> GetArticle([FromServices] IQueryExecutor queryExecutor, [FromRoute] int articleId, CancellationToken cancellationToken) {
            var result = await queryExecutor.Execute(new GetArticleQuery(articleId), cancellationToken);
            return result switch {
                null => TypedResults.NotFound(),
                _ => TypedResults.Ok(result)
            };
        }
    }
}

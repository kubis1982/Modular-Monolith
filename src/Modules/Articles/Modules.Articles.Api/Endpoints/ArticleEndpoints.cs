namespace ModularMonolith.Modules.Articles.Endpoints
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Http.HttpResults;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Routing;
    using ModularMonolith.Modules.Articles.Commands.Articles;
    using ModularMonolith.Modules.Articles.Queries.Articles;
    using ModularMonolith.Modules.Articles.Requests.Articles;
    using ModularMonolith.Shared.CQRS.Commands;
    using ModularMonolith.Shared.CQRS.Queries;
    using ModularMonolith.Shared.Modules;
    using ModularMonolith.Shared.Modules.Endpoints.Responses;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    internal class ArticleEndpoints : IModuleEndpoints
    {
        public void AddRoutes(IEndpointRouteBuilder endpointRouteBuilder)
        {
            var articlesGroup = endpointRouteBuilder.MapGroup("articles");

            articlesGroup.MapPost("", CreateArticle);

            var articleIdGroup = articlesGroup.MapGroup("{articleId:int}");

            articleIdGroup.MapPut("", UpdateArticle);
            articleIdGroup.MapDelete("", DeleteArticle);
            articleIdGroup.MapPatch("block", BlockArticle);
            articleIdGroup.MapPatch("unblock", UnblockArticle);

            endpointRouteBuilder.MapGet("articles", GetArticles);
            endpointRouteBuilder.MapGet("articles/{articleId}", GetArticle);
        }

        /// <summary>
        /// Creates a new article.
        /// </summary>
        /// <param name="commandExecutor">The command executor to execute the create article command.</param>
        /// <param name="request">The request containing the details of the article to be created.</param>
        /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
        /// <returns>Returns an Ok result containing the identity response of the created article.</returns>
        private async Task<Ok<IdentityResponse>> CreateArticle([FromServices] ICommandExecutor commandExecutor, [FromBody] CreateArticleRequest request, CancellationToken cancellationToken)
        {
            var article = await commandExecutor.Execute(new CreateArticleCommand(request.Code, request.Name, request.Unit) { Description = request.Description }, cancellationToken);
            return TypedResults.Ok((IdentityResponse)article);
        }


        /// <summary>
        /// Updates an existing article.
        /// </summary>
        /// <param name="commandExecutor">The command executor to execute the update article command.</param>
        /// <param name="articleId">The ID of the article to be updated.</param>
        /// <param name="request">The request containing the updated details of the article.</param>
        /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
        /// <returns>Returns an Ok result if the article was successfully updated.</returns>
        private async Task<Ok> UpdateArticle([FromServices] ICommandExecutor commandExecutor, [FromRoute] int articleId, [FromBody] UpdateArticleRequest request, CancellationToken cancellationToken)
        {
            await commandExecutor.Execute(new UpdateArticleCommand(articleId, request.Code, request.Name, request.Unit) { Description = request.Description }, cancellationToken);
            return TypedResults.Ok();
        }

        /// <summary>
        /// Deletes an existing article.
        /// </summary>
        /// <param name="commandExecutor">The command executor to execute the delete article command.</param>
        /// <param name="articleId">The ID of the article to be deleted.</param>
        /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
        /// <returns>Returns a NoContent result if the article was successfully deleted.</returns>
        private async Task<NoContent> DeleteArticle([FromServices] ICommandExecutor commandExecutor, [FromRoute] int articleId, CancellationToken cancellationToken)
        {
            await commandExecutor.Execute(new DeleteArticleCommand(articleId), cancellationToken);
            return TypedResults.NoContent();
        }

        /// <summary>
        /// Blocks an existing article.
        /// </summary>
        /// <param name="commandExecutor">The command executor to execute the block article command.</param>
        /// <param name="articleId">The ID of the article to be blocked.</param>
        /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
        /// <returns>Returns an Ok result if the article was successfully blocked.</returns>
        private async Task<Ok> BlockArticle([FromServices] ICommandExecutor commandExecutor, [FromRoute] int articleId, CancellationToken cancellationToken)
        {
            await commandExecutor.Execute(new BlockArticleCommand(articleId), cancellationToken);
            return TypedResults.Ok();
        }

        private async Task<Ok> UnblockArticle([FromServices] ICommandExecutor commandExecutor, [FromRoute] int articleId, CancellationToken cancellationToken)
        {
            await commandExecutor.Execute(new UnblockArticleCommand(articleId), cancellationToken);
            return TypedResults.Ok();
        }

        /// <summary>
        /// Gets all articles
        /// </summary>
        /// <param name="queryExecutor"></param>
        /// <param name="arguments"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        private async Task<Ok<IEnumerable<GetArticlesQueryResult>>> GetArticles([FromServices] IQueryExecutor queryExecutor, [AsParameters] GetArticlesQueryRequest arguments, CancellationToken cancellationToken)
        {
            var results = await queryExecutor.Execute(new GetArticlesQuery(arguments.IncludeBlocked ?? false), cancellationToken);
            return TypedResults.Ok(results);
        }

        /// <summary>
        /// Gets a single article by identity
        /// </summary>
        /// <param name="queryExecutor"></param>
        /// <param name="articleId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        private async Task<Results<Ok<GetArticlesQueryResult>, NotFound>> GetArticle([FromServices] IQueryExecutor queryExecutor, [FromRoute] int articleId, CancellationToken cancellationToken)
        {
            var result = (await queryExecutor.Execute(new GetArticlesQuery(articleId), cancellationToken)).FirstOrDefault();
            return result switch
            {
                null => TypedResults.NotFound(),
                _ => TypedResults.Ok(result),
            };
        }
    }
}

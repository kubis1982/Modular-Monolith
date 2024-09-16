namespace ModularMonolith.Modules.AccessManagement.Endpoints
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Http.HttpResults;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Routing;
    using ModularMonolith.Modules.AccessManagement.CQRS.Commands.Users;
    using ModularMonolith.Modules.AccessManagement.CQRS.Queries.Users;
    using ModularMonolith.Modules.AccessManagement.Endpoints.Requests.Users;
    using ModularMonolith.Shared.CQRS.Commands;
    using ModularMonolith.Shared.CQRS.Queries;
    using ModularMonolith.Shared.Modules;
    using ModularMonolith.Shared.Modules.Endpoints.Responses;
    using ModularMonolith.Shared.Security;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Represents the endpoints for managing users.
    /// </summary>
    internal class UserEndpoints : IModuleEndpoints
    {
        /// <summary>
        /// Adds the routes for user endpoints.
        /// </summary>
        /// <param name="endpointRouteBuilder">The endpoint route builder.</param>
        public void AddRoutes(IEndpointRouteBuilder endpointRouteBuilder)
        {
            endpointRouteBuilder.MapPost("/users", CreateUser);
            endpointRouteBuilder.MapPut("/users/{userId}", UpdateUser);
            endpointRouteBuilder.MapDelete("/users/{userId}", DeleteUser);
            endpointRouteBuilder.MapPatch("/users/{userId}/change-password", ChangePassword);
            endpointRouteBuilder.MapPatch("/users/{userId}/activate", ActivateUser);
            endpointRouteBuilder.MapPatch("/users/{userId}/deactivate", DeactivateUser);
            endpointRouteBuilder.MapGet("/users/{userId}", GetUser);
        }

        /// <summary>
        /// Creates a new user.
        /// </summary>
        /// <param name="commandExecutor">The command executor.</param>
        /// <param name="moduleDefinition">The module definition.</param>
        /// <param name="request">The create user request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The created user identity response.</returns>
        private async Task<Created<IdentityResponse>> CreateUser([FromServices] ICommandExecutor commandExecutor, [FromServices] ModuleDefinition moduleDefinition, [FromBody] CreateUserRequest request, CancellationToken cancellationToken)
        {
            var user = await commandExecutor.Execute(new CreateUserCommand(request.Email, request.Password, request.FirstName, request.MiddleName, request.LastName), cancellationToken);
            return TypedResults.Created(moduleDefinition.GetUrl($"/users/{user.Id}"), (IdentityResponse)user);
        }

        /// <summary>
        /// Updates an existing user.
        /// </summary>
        /// <param name="commandExecutor">The command executor.</param>
        /// <param name="userId">The user ID.</param>
        /// <param name="request">The update user request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>An OK response.</returns>
        private async Task<Ok> UpdateUser([FromServices] ICommandExecutor commandExecutor, [FromRoute] int userId, [FromBody] UpdateUserRequest request, CancellationToken cancellationToken)
        {
            await commandExecutor.Execute(new UpdateUserCommand(userId, request.FirstName, request.MiddleName, request.LastName), cancellationToken);
            return TypedResults.Ok();
        }

        /// <summary>
        /// Deletes a user.
        /// </summary>
        /// <param name="commandExecutor">The command executor.</param>
        /// <param name="userId">The user ID.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A NoContent response.</returns>
        private async Task<NoContent> DeleteUser([FromServices] ICommandExecutor commandExecutor, [FromRoute] int userId, CancellationToken cancellationToken)
        {
            await commandExecutor.Execute(new DeleteUserCommand(userId), cancellationToken);
            return TypedResults.NoContent();
        }

        /// <summary>
        /// Changes the password for a user.
        /// </summary>
        /// <param name="commandExecutor">The command executor.</param>
        /// <param name="userId">The user ID.</param>
        /// <param name="request">The change password request.</param>
        /// <param name="userContext">The user context.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>An OK response.</returns>
        private async Task<Ok> ChangePassword([FromServices] ICommandExecutor commandExecutor, [FromRoute] int userId, [FromBody] ChangePasswordRequest request, [FromServices] IUserContext userContext, CancellationToken cancellationToken)
        {
            await commandExecutor.Execute(new ChangePasswordCommand(userId, request.Password), cancellationToken);
            return TypedResults.Ok();
        }

        /// <summary>
        /// Deactivates a user.
        /// </summary>
        /// <param name="commandExecutor">The command executor.</param>
        /// <param name="userId">The user ID.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>An OK response.</returns>
        private async Task<Ok> DeactivateUser([FromServices] ICommandExecutor commandExecutor, [FromRoute] int userId, CancellationToken cancellationToken)
        {
            await commandExecutor.Execute(new DeactivateUserCommand(userId), cancellationToken);
            return TypedResults.Ok();
        }

        /// <summary>
        /// Activates a user.
        /// </summary>
        /// <param name="commandExecutor">The command executor.</param>
        /// <param name="userId">The user ID.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>An OK response.</returns>
        private async Task<Ok> ActivateUser([FromServices] ICommandExecutor commandExecutor, [FromRoute] int userId, CancellationToken cancellationToken)
        {
            await commandExecutor.Execute(new ActivateUserCommand(userId), cancellationToken);
            return TypedResults.Ok();
        }

        /// <summary>
        /// Retrieves a user by ID.
        /// </summary>
        /// <param name="queryExecutor">The query executor.</param>
        /// <param name="userId">The user ID.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A results object containing either an OK response with the user query result or a NotFound response.</returns>
        private async Task<Results<Ok<GetUserQueryResult>, NotFound>> GetUser([FromServices] IQueryExecutor queryExecutor, [FromRoute] int userId, CancellationToken cancellationToken)
        {
            var user = await queryExecutor.Execute(new GetUserQuery(userId), cancellationToken);
            return user switch
            {
                null => TypedResults.NotFound(),
                _ => TypedResults.Ok(user)
            };
        }
    }
}

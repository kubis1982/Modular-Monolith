namespace ModularMonolith.Modules.AccessManagement.Endpoints
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Http.HttpResults;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Routing;
    using ModularMonolith.Modules.AccessManagement.Endpoints.Requests.Sessions;
    using ModularMonolith.Modules.AccessManagement.Endpoints.Responses.Sessions;
    using ModularMonolith.Modules.AccessManagement.Services;
    using ModularMonolith.Shared.Modules;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Represents the endpoints for managing user sessions.
    /// </summary>
    internal class SessionEndpoints : IModuleEndpoints
    {
        /// <summary>
        /// Adds the session routes to the endpoint route builder.
        /// </summary>
        /// <param name="endpointRouteBuilder">The endpoint route builder.</param>
        public void AddRoutes(IEndpointRouteBuilder endpointRouteBuilder)
        {
            endpointRouteBuilder.MapPost("/sessions", CreateSession);
            endpointRouteBuilder.MapPatch("/sessions/{refreshToken}/refresh", RefreshSession);
        }

        /// <summary>
        /// Creates a new user session.
        /// </summary>
        /// <param name="sessionService">The session service.</param>
        /// <param name="moduleDefinition">The module definition.</param>
        /// <param name="request">The create session request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The results of creating a session.</returns>
        private async Task<Results<Ok<SessionResponse>, Created<SessionResponse>>> CreateSession([FromServices] ISessionService sessionService, [FromServices] ModuleDefinition moduleDefinition, [FromBody] CreateSessionRequest request, CancellationToken cancellationToken)
        {
            var result = await sessionService.CreateSessionAsync(request.Name, request.Password, cancellationToken);
            SessionResponse response = new()
            {
                AccessToken = result.accessToken,
                RefreshToken = result.refreshToken,
                ExpiryDate = result.expiryDate,
                RefreshTokenExpiryDate = result.refreshTokenExpiryDate
            };
            return TypedResults.Created(moduleDefinition.GetUrl($"/sessions/{result.sessionId}"), response);
        }

        /// <summary>
        /// Refreshes a user session.
        /// </summary>
        /// <param name="sessionService">The session service.</param>
        /// <param name="refreshToken">The refresh token.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The refreshed session response.</returns>
        private static async Task<Ok<SessionResponse>> RefreshSession([FromServices] ISessionService sessionService, [FromRoute] string refreshToken, CancellationToken cancellationToken)
        {
            var result = await sessionService.RefreshSessionAsync(refreshToken, cancellationToken);
            SessionResponse response = new()
            {
                AccessToken = result.accessToken,
                RefreshToken = result.refreshToken,
                ExpiryDate = result.expiryDate,
                RefreshTokenExpiryDate = result.refreshTokenExpiryDate
            };
            return TypedResults.Ok(response);
        }
    }
}

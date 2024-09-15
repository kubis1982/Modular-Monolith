namespace ModularMonolith.Modules.AccessManagement.Services
{
    using Microsoft.Extensions.Options;
    using ModularMonolith.Modules.AccessManagement.CQRS.Commands.Users;
    using ModularMonolith.Modules.AccessManagement.CQRS.Queries.Users;
    using ModularMonolith.Modules.AccessManagement.Exceptions.Users;
    using ModularMonolith.Shared.CQRS.Commands;
    using ModularMonolith.Shared.CQRS.Queries;
    using ModularMonolith.Shared.Security;
    using ModularMonolith.Shared.Time;
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using CreateSessionResult = (string accessToken, System.DateTime expiryDate, string refreshToken, System.DateTime refreshTokenExpiryDate, int sessionId);
    using RefreshSessionResult = (string accessToken, System.DateTime expiryDate, string refreshToken, System.DateTime refreshTokenExpiryDate);

    internal class SessionService(ICommandExecutor commandExecutor, IQueryExecutor queryExecutor, IUserContext userIdentity,
            IJwtProvider jwtProvider, IClock clock, IOptionsMonitor<AuthOptions> authOptions) : ISessionService
    {
        private static string GenerateRefreshToken()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            var list = Enumerable.Repeat(0, 64).Select(x => chars[random.Next(chars.Length)]);
            return string.Join("", list);
        }
        /// <summary>
        /// Refreshes the session for the current user with the specified refresh token.
        /// </summary>
        /// <param name="refreshToken">The refresh token.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The refreshed session result.</returns>
        public async Task<RefreshSessionResult> RefreshSessionAsync(string refreshToken, CancellationToken cancellationToken)
        {
            var user = await queryExecutor.Execute(new GetUserQuery(userIdentity.Id), cancellationToken) ?? throw new UserNotFoundException();
            JwtClaimValues jwtClaimValues = new()
            {
                UserId = userIdentity.Id,
                UserName = user.Email ?? userIdentity.Name,
                UserEmail = user.Email ?? userIdentity.Email,
                SessionId = userIdentity.SessionId
            };
            return await CreateSessionAsync(refreshToken, jwtClaimValues, cancellationToken);
        }

        private async Task<RefreshSessionResult> CreateSessionAsync(string refreshToken, JwtClaimValues jwtClaimValues, CancellationToken cancellationToken)
        {
            string newRefreshToken = GenerateRefreshToken();
            DateTime expiryDate = clock.Now.AddMinutes(authOptions.CurrentValue.TokenValidityInMinutes);
            DateTime refreshTokenExpiryDate = clock.Now.AddDays(authOptions.CurrentValue.RefreshTokenValidityInDays);
            await commandExecutor.Execute(new RefreshSessionCommand(userIdentity.SessionId, expiryDate, refreshToken, newRefreshToken, refreshTokenExpiryDate), cancellationToken);
            var session = await queryExecutor.Execute(new GetSessionQuery(userIdentity.SessionId), cancellationToken) ?? throw new SessionNotFoundException();
            string token = jwtProvider.GenerateToken(jwtClaimValues, session.ExpiryDate);
            return (token, session.ExpiryDate, session.RefreshToken ?? "", session.RefreshTokenExpiryDate ?? DateTime.MinValue);
        }

        /// <summary>
        /// Creates a new session for a user with the specified name, password, and optional new password.
        /// </summary>
        /// <param name="email">The email of the user.</param>
        /// <param name="password">The password of the user.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The created session result.</returns>
        public async Task<CreateSessionResult> CreateSessionAsync(string email, string password, CancellationToken cancellationToken)
        {
            string refreshToken = GenerateRefreshToken();
            DateTime expiryDate = clock.Now.AddMinutes(authOptions.CurrentValue.TokenValidityInMinutes);
            DateTime refreshTokenExpiryDate = clock.Now.AddDays(authOptions.CurrentValue.RefreshTokenValidityInDays);
            CreateSessionCommand createSessionCommand = new(email, password, expiryDate, refreshToken, refreshTokenExpiryDate);
            EntityIdentityResult sessionId = await commandExecutor.Execute(createSessionCommand, cancellationToken);
            GetSessionQueryResult session = (await queryExecutor.Execute(new GetSessionQuery(sessionId.Id), cancellationToken)) ?? throw new SessionNotFoundException();
            GetUserQueryResult user = (await queryExecutor.Execute(new GetUserQuery(session.CreatedBy), cancellationToken)) ?? throw new UserNotFoundException();
            JwtClaimValues jwtClaimValues = new()
            {
                UserId = user.Id,
                UserEmail = user.Email ?? string.Empty,
                UserName = user.Email ?? string.Empty,
                SessionId = sessionId.Id
            };
            string token = jwtProvider.GenerateToken(jwtClaimValues, session.ExpiryDate);
            return (token, session.ExpiryDate, session.RefreshToken ?? "", session.RefreshTokenExpiryDate ?? DateTime.MinValue, sessionId.Id);
        }
    }
   
    internal interface ISessionService
    {
        /// <summary>
        /// Creates a new session for a user with the specified name, password, and optional new password.
        /// </summary>
        /// <param name="name">The name of the user.</param>
        /// <param name="password">The password of the user.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The created session result.</returns>
        Task<CreateSessionResult> CreateSessionAsync(string name, string password, CancellationToken cancellationToken);

        /// <summary>
        /// Refreshes the session for the current user with the specified refresh token.
        /// </summary>
        /// <param name="refreshToken">The refresh token.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The refreshed session result.</returns>
        Task<RefreshSessionResult> RefreshSessionAsync(string refreshToken, CancellationToken cancellationToken);
    }
}

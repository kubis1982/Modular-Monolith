namespace ModularMonolith.Modules.AccessManagement.Services
{
    using ModularMonolith.Shared.CQRS.Commands;
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using CreateSessionResult = (string accessToken, System.DateTime expiryDate, string refreshToken, System.DateTime refreshTokenExpiryDate, int sessionId);
    using RefreshSessionResult = (string accessToken, System.DateTime expiryDate, string refreshToken, System.DateTime refreshTokenExpiryDate);

    internal class SessionService : ISessionService
    {
        public Task<(string accessToken, DateTime expiryDate, string refreshToken, DateTime refreshTokenExpiryDate, int sessionId)> CreateSessionAsync(string name, string password, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<(string accessToken, DateTime expiryDate, string refreshToken, DateTime refreshTokenExpiryDate)> RefreshSessionAsync(string refreshToken, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }

    //    internal class SessionService(ICommandExecutor commandExecutor, IQueryExecutor queryExecutor, IUserContext userIdentity,
    //        IJwtProvider jwtProvider, IClock clock, IOptionsMonitor<AuthOptions> authOptions) : ISessionService
    //    {

    //        /// <summary>
    //        /// Refreshes the session for a user with the specified refresh token and role ID.
    //        /// </summary>
    //        /// <param name="refreshToken">The refresh token.</param>
    //        /// <param name="roleId">The role ID.</param>
    //        /// <param name="cancellationToken">The cancellation token.</param>
    //        /// <returns>The refreshed session result.</returns>
    //        public async Task<RefreshSessionResult> RefreshSessionAsync(string refreshToken, int roleId, CancellationToken cancellationToken)
    //        {
    //            var user = (await queryExecutor.ExecuteAsync(new GetUsersQuery(userIdentity.Id), cancellationToken)).SingleOrDefault()
    //                ?? throw new InvalidOperationException("User not found");
    //            if (!user.Roles.Any(n => n == roleId))
    //                throw new InvalidOperationException("User does not have the required role");
    //            JwtClaimValues jwtClaimValues = new()
    //            {
    //                UserId = userIdentity.Id,
    //                UserName = user.Name ?? userIdentity.Name,
    //                SessionId = userIdentity.SessionId,
    //                Roles = user.Roles,
    //                RoleId = roleId
    //            };
    //            return await CreateSessionAsync(refreshToken, jwtClaimValues, cancellationToken);
    //        }

    //        /// <summary>
    //        /// Refreshes the session for the current user with the specified refresh token.
    //        /// </summary>
    //        /// <param name="refreshToken">The refresh token.</param>
    //        /// <param name="cancellationToken">The cancellation token.</param>
    //        /// <returns>The refreshed session result.</returns>
    //        public async Task<RefreshSessionResult> RefreshSessionAsync(string refreshToken, CancellationToken cancellationToken)
    //        {
    //            var user = (await queryExecutor.Get(new GetUsersQuery(userIdentity.Id), cancellationToken)).SingleOrDefault()
    //                ?? throw new InvalidOperationException("User not found");
    //            JwtClaimValues jwtClaimValues = new()
    //            {
    //                UserId = userIdentity.Id,
    //                UserName = user.Name ?? userIdentity.Name,
    //                SessionId = userIdentity.SessionId,
    //                Roles = user.Roles,
    //                RoleId = userIdentity.RoleId
    //            };
    //            return await CreateSessionAsync(refreshToken, jwtClaimValues, cancellationToken);
    //        }

    //        private async Task<RefreshSessionResult> CreateSessionAsync(string refreshToken, JwtClaimValues jwtClaimValues, CancellationToken cancellationToken)
    //        {
    //            string newRefreshToken = TokenGenerator.GenerateRefreshToken();
    //            DateTime expiryDate = clock.Now.AddMinutes(authOptions.CurrentValue.TokenValidityInMinutes);
    //            DateTime refreshTokenExpiryDate = clock.Now.AddDays(authOptions.CurrentValue.RefreshTokenValidityInDays);
    //            await commandExecutor.Execute(new RefreshSessionCommand(userIdentity.SessionId, expiryDate, refreshToken, newRefreshToken, refreshTokenExpiryDate), cancellationToken);
    //            var session = await queryExecutor.ExecuteAsync(new GetSessionQuery(userIdentity.SessionId), cancellationToken);
    //            string token = jwtProvider.GenerateToken(jwtClaimValues, session.ExpiryDate);
    //            return (token, session.ExpiryDate, session.RefreshToken ?? "", session.RefreshTokenExpiryDate ?? DateTime.MinValue);
    //        }

    //        /// <summary>
    //        /// Creates a new session for a user with the specified name, password, and optional new password.
    //        /// </summary>
    //        /// <param name="name">The name of the user.</param>
    //        /// <param name="password">The password of the user.</param>
    //        /// <param name="newPassword">The new password of the user (optional).</param>
    //        /// <param name="cancellationToken">The cancellation token.</param>
    //        /// <returns>The created session result.</returns>
    //        public async Task<CreateSessionResult> CreateSessionAsync(string name, string password, string? newPassword, CancellationToken cancellationToken)
    //        {
    //            string refreshToken = TokenGenerator.GenerateRefreshToken();
    //            DateTime expiryDate = clock.Now.AddMinutes(authOptions.CurrentValue.TokenValidityInMinutes);
    //            DateTime refreshTokenExpiryDate = clock.Now.AddDays(authOptions.CurrentValue.RefreshTokenValidityInDays);
    //            CreateSessionCommand createSessionCommand = new(name, password, expiryDate, refreshToken, refreshTokenExpiryDate) { NewPassword = newPassword };
    //            EntityIdentityResult sessionId = await commandExecutor.ExecuteAsync(createSessionCommand, cancellationToken);
    //            GetSessionQueryResult session = (await queryExecutor.ExecuteAsync(new GetSessionQuery(sessionId.Id), cancellationToken));
    //            GetUsersQueryResult user = (await queryExecutor.ExecuteAsync(new GetUsersQuery(session.UserId), cancellationToken)).Single();
    //            JwtClaimValues jwtClaimValues = new()
    //            {
    //                UserId = user.Id,
    //                UserName = user.Name ?? string.Empty,
    //                SessionId = sessionId.Id,
    //                Roles = user.Roles,
    //                RoleId = user.Role.Id
    //            };
    //            string token = jwtProvider.GenerateToken(jwtClaimValues, session.ExpiryDate);
    //            return (token, session.ExpiryDate, session.RefreshToken ?? "", session.RefreshTokenExpiryDate ?? DateTime.MinValue, sessionId.Id);
    //        }
    //    }

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

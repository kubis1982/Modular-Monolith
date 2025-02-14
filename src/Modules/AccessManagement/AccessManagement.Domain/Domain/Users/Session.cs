﻿namespace ModularMonolith.Modules.AccessManagement.Domain.Users
{
    using ModularMonolith.Shared.Kernel.Types;
    using ModularMonolith.Shared.Time;
    using System;

    /// <summary>
    /// Represents a session entity in the access management domain.
    /// </summary>
    public sealed class Session : DomainEntity<SessionId, int, EntityType>
    {
        /// <summary>
        /// Gets the expiry date of the session.
        /// </summary>
        internal DateTime ExpirationDate { get; private set; }

        /// <summary>
        /// Gets the refresh token associated with the session.
        /// </summary>
        internal RefreshToken? RefreshToken { get; private set; }

        /// <summary>
        /// Gets the user who killed the session.
        /// </summary>
        internal User? Killer { get; private set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        private Session() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        /// <summary>
        /// Initializes a new instance of the <see cref="Session"/> class with the specified expiry date and refresh token.
        /// </summary>
        /// <param name="expirationDate">The expiry date of the session.</param>
        /// <param name="refreshToken">The refresh token associated with the session.</param>
        private Session(DateTime expirationDate, RefreshToken refreshToken) : this()
        {
            ExpirationDate = expirationDate;
            RefreshToken = refreshToken;
        }

        /// <summary>
        /// Kills the session by setting the killer, clearing the refresh token, and updating the expiry date.
        /// </summary>
        /// <param name="user">The user who killed the session.</param>
        /// <param name="clock">The clock used to get the current time.</param>
        internal void Kill(User user, IClock clock)
        {
            if (Killer?.Id.Id > 0)
            {
                throw new SessionHasAlreadyBeenKilledException();
            }
            Killer = user;
            RefreshToken = null;
            ExpirationDate = clock.Now;
        }

        /// <summary>
        /// Refreshes the session with a new refresh token and expiry date.
        /// </summary>
        /// <param name="refreshToken">The current refresh token.</param>
        /// <param name="newRefreshToken">The new refresh token.</param>
        /// <param name="newExpirationDate">The new expiry date.</param>
        /// <param name="clock">The clock used to get the current time.</param>
        internal void Refresh(string refreshToken, RefreshToken newRefreshToken, DateTime newExpirationDate, IClock clock)
        {
            if (!RefreshToken!.Token.Equals(refreshToken))
            {
                throw new InvalidRefreshTokenException();
            }

            if (RefreshToken.ExpirationDate < clock.Now)
            {
                throw new RefreshTokenHasExpiredException();
            }
            RefreshToken = newRefreshToken;
            ExpirationDate = newExpirationDate;
        }

        /// <summary>
        /// Creates a new session with the specified expiry date and refresh token.
        /// </summary>
        /// <param name="expirationDate">The expiry date of the session.</param>
        /// <param name="refreshToken">The refresh token associated with the session.</param>
        /// <returns>The created session.</returns>
        internal static Session Create(DateTime expirationDate, RefreshToken refreshToken)
        {
            return new(expirationDate, refreshToken);
        }
    }
}

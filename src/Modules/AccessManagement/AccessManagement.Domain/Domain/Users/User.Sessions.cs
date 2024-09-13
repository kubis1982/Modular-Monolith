using Kubis1982.Modules.AccessManagement.Domain.Users.Events;
using Kubis1982.Shared.Time;
using System;
using System.Linq;

namespace Kubis1982.Modules.AccessManagement.Domain.Users
{
    /// <summary>
    /// Represents a user in the access control domain.
    /// </summary>
    partial class User
	{
		/// <summary>
		/// Gets the session with the specified session ID.
		/// </summary>
		/// <param name="sessionId">The session ID.</param>
		/// <returns>The session with the specified session ID.</returns>
		private Session GetSession(SessionId sessionId)
		{
			return sessions.SingleOrDefault(n => n.Id == sessionId) ?? throw new SessionNotFoundException(sessionId);
		}

		/// <summary>
		/// Creates a new session for the user.
		/// </summary>
		/// <param name="userPassword">The user's password.</param>
		/// <param name="expiryDate">The expiry date of the session.</param>
		/// <param name="refreshToken">The refresh token for the session.</param>
		public Session CreateSession(UserPassword userPassword, DateTime expiryDate, RefreshToken refreshToken)
		{
			CheckPassword(userPassword);
			Session session = Session.Create(expiryDate, refreshToken);
			sessions.Add(session);
			AddEvent(new SessionCreatedEvent(this, session, expiryDate));
			return session;
		}

		/// <summary>
		/// Refreshes the session with the specified session ID.
		/// </summary>
		/// <param name="sessionId">The session ID.</param>
		/// <param name="refreshToken">The new refresh token for the session.</param>
		/// <param name="expiryDate">The new expiry date of the session.</param>
		/// <param name="newRefreshToken"></param>
		/// <param name="clock">The clock used to determine the current time.</param>
		public void RefreshSession(SessionId sessionId, string refreshToken, DateTime expiryDate, RefreshToken newRefreshToken, IClock clock)
		{
			Session session = GetSession(sessionId);
			session.Refresh(refreshToken, newRefreshToken, expiryDate, clock);
			AddEvent(new SessionExpiryDateExtendedEvent(this, session, expiryDate));
		}
	}
}

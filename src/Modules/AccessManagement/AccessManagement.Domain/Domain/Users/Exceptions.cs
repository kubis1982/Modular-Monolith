namespace ModularMonolith.Modules.AccessManagement.Domain.Users
{
    using ModularMonolith.Shared.Exceptions;
    using System;

    /// <summary>
    /// Exception thrown when attempting to perform an action on the current user.
    /// </summary>
    public sealed class ActionOnCurrentUserException() : AppException("Cannot perform action on the current user.");
    /// <summary>
    /// Exception thrown when attempting to delete an administrator, which is not allowed.
    /// </summary>
    public sealed class DeletingAdministratorException() : AppException("Deleting administrator is not allowed.");
    /// <summary>
    /// Exception thrown when an incorrect email is provided.
    /// </summary>
    /// <param name="email">The incorrect email.</param>
    public sealed class IncorrectEmailException(string? email) : AppException($"Incorrect email: {email}");
    /// <summary>
    /// Exception thrown when an empty password is provided.
    /// </summary>
    public sealed class IncorrectPasswordException() : AppException("Password cannot be empty");
    /// <summary>
    /// Exception thrown when an incorrect user password is provided.
    /// </summary>
    public sealed class IncorrectUserPasswordException() : AppException("Incorrect password.");
    /// <summary>
    /// Exception thrown when two passwords are the same.
    /// </summary>
    public sealed class PasswordsCannotBeTheSameException() : AppException("Passwords cannot be the same.");
    /// <summary>
    /// Exception thrown when a token is empty.
    /// </summary>
    public sealed class TokenIsEmptyException() : AppException("Token is empty");
    /// <summary>
    /// Exception thrown when a user is inactive.
    /// </summary>
    public sealed class UserIsUnactiveException() : AppException("User is inactive.");

    /// <summary>
    /// Exception thrown when a session has already been killed.
    /// </summary>
    public sealed class SessionHasAlreadyBeenKilledException() : AppException("Session has already been killed.");
    /// <summary>
    /// Exception thrown when an invalid refresh token is provided.
    /// </summary>
    public sealed class InvalidRefreshTokenException() : AppException("Invalid refresh token.");
    /// <summary>
    /// Exception thrown when a refresh token has expired.
    /// </summary>
    public sealed class RefreshTokenHasExpiredException() : AppException("Refresh token has expired.");
    /// <summary>
    /// Exception thrown when a user has active sessions.
    /// </summary>
    public sealed class UserHasSessionsException() : AppException("User has active sessions.");
    /// <summary>
    /// Exception thrown when a session is not found.
    /// </summary>
    /// <param name="sessionId">The session ID.</param>
    public sealed class SessionNotFoundException(SessionId sessionId) : AppException($"Session not found: {sessionId}");

    /// <summary>
    /// Exception thrown when an incorrect user token is provided.
    /// </summary>
    public sealed class IncorrectUserTokenException() : AppException($"Incorrect user token");

    /// <summary>
    /// Exception thrown when an invalid user token is provided.
    /// </summary>
    /// <param name="token">The invalid user token.</param>
    public sealed class InvalidUserTokenException(Guid? token) : AppException($"Token '{token}' is invalid");
}

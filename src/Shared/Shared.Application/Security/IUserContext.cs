namespace ModularMonolith.Shared.Security
{
    /// <summary>
    /// Represents the user context.
    /// </summary>
    public interface IUserContext
    {
        /// <summary>
        /// Gets the user ID.
        /// </summary>
        int Id { get; }

        /// <summary>
        /// Gets the user name.
        /// </summary>
        string Email { get; }

        /// <summary>
        /// Gets the session ID.
        /// </summary>
        int SessionId { get; }

        /// <summary>
        /// Gets the user name.
        /// </summary>
        string Name { get; }
    }
}

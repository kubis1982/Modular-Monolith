namespace Kubis1982.Shared.Security
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
        string Name { get; }

        /// <summary>
        /// Gets the session ID.
        /// </summary>
        int SessionId { get; }

        /// <summary>
        /// Gets the roles assigned to the user.
        /// </summary>
        int[] Roles { get; }

        /// <summary>
        /// Gets the role ID.
        /// </summary>
        int RoleId { get; }
    }
}

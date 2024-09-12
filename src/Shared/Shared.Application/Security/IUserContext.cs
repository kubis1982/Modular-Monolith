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
        int UserId { get; }

        /// <summary>
        /// Gets the user name.
        /// </summary>
        string UserName { get; }
    }
}

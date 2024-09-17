namespace ModularMonolith.Shared
{
    using ModularMonolith.Shared.Security;

    /// <summary>
    /// Represents a test user context.
    /// </summary>
    public class TestUserContext : IUserContext
    {
        /// <summary>
        /// Gets or sets the session ID.
        /// </summary>
        public int SessionId { get; set; } = 1;

        /// <summary>
        /// Gets or sets the user ID.
        /// </summary>
        public int Id { get; set; } = 1;

        /// <summary>
        /// Gets or sets the user email.
        /// </summary>
        public string Email { get; set; } = $"administrator@{SystemInformation.DomainName}";

        /// <summary>
        /// Gets or sets the user name.
        /// </summary>
        public string Name { get; set; } = $"administrator@{SystemInformation.DomainName}";
    }
}

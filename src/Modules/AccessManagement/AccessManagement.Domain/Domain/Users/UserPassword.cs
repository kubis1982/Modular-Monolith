namespace ModularMonolith.Modules.AccessManagement.Domain.Users
{
    /// <summary>
    /// Represents a user password.
    /// </summary>
    public sealed record UserPassword
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserPassword"/> class.
        /// </summary>
        private UserPassword()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserPassword"/> class with the specified hash.
        /// </summary>
        /// <param name="hash">The password hash.</param>
        /// <exception cref="IncorrectPasswordException">Thrown when the hash is null, empty, or whitespace.</exception>
        private UserPassword(string? hash)
        {
            if (string.IsNullOrWhiteSpace(hash))
            {
                throw new IncorrectPasswordException();
            }

            Hash = hash ?? string.Empty;
        }

        /// <summary>
        /// Gets the password hash.
        /// </summary>
        public string Hash { get; private set; } = string.Empty;

        /// <summary>
        /// Creates a new <see cref="UserPassword"/> instance with the specified password and password hasher.
        /// </summary>
        /// <param name="password">The password to create the hash from.</param>
        /// <param name="passwordHasher">The password hasher.</param>
        /// <returns>A new <see cref="UserPassword"/> instance.</returns>
        public static UserPassword Create(string? password, IPasswordHasher passwordHasher)
        {
            string hash = passwordHasher.Compute(password);
            return new UserPassword(hash);
        }

        /// <summary>
        /// Creates a new <see cref="UserPassword"/> instance with the specified hash.
        /// </summary>
        /// <param name="hash">The password hash.</param>
        /// <returns>A new <see cref="UserPassword"/> instance.</returns>
        public static UserPassword Of(string? hash) => new(hash);

        /// <summary>
        /// Converts a string to a <see cref="UserPassword"/> instance.
        /// </summary>
        /// <param name="hash">The password hash.</param>
        /// <returns>A new <see cref="UserPassword"/> instance.</returns>
        public static explicit operator UserPassword(string hash) => Of(hash);
    }
}

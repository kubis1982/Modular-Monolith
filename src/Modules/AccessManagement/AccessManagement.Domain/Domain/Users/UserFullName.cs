namespace ModularMonolith.Modules.AccessManagement.Domain.Users
{
    /// <summary>
    /// Represents the full name of a user.
    /// </summary>
    public sealed record UserFullName
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserFullName"/> class.
        /// </summary>
        private UserFullName()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserFullName"/> class with the specified first name, middle name, and last name.
        /// </summary>
        /// <param name="firstName">The first name.</param>
        /// <param name="middleName">The middle name.</param>
        /// <param name="lastName">The last name.</param>
        private UserFullName(string? firstName, string? middleName, string? lastName)
        {
            FirstName = firstName ?? string.Empty;
            MiddleName = middleName ?? string.Empty;
            LastName = lastName ?? string.Empty;
        }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        public string FirstName { get; private set; } = string.Empty;

        /// <summary>
        /// Gets or sets the middle name.
        /// </summary>
        public string MiddleName { get; private set; } = string.Empty;

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        public string LastName { get; private set; } = string.Empty;

        /// <summary>
        /// Gets an empty <see cref="UserFullName"/> instance.
        /// </summary>
        public static UserFullName Empty => Create(string.Empty);

        /// <summary>
        /// Creates a new <see cref="UserFullName"/> instance with the specified last name.
        /// </summary>
        /// <param name="lastName">The last name.</param>
        /// <returns>A new <see cref="UserFullName"/> instance.</returns>
        public static UserFullName Create(string? lastName)
        {
            return new(string.Empty, string.Empty, lastName);
        }

        /// <summary>
        /// Creates a new <see cref="UserFullName"/> instance with the specified first name and last name.
        /// </summary>
        /// <param name="firstName">The first name.</param>
        /// <param name="lastName">The last name.</param>
        /// <returns>A new <see cref="UserFullName"/> instance.</returns>
        public static UserFullName Create(string? firstName, string? lastName)
        {
            return new(firstName, string.Empty, lastName);
        }

        /// <summary>
        /// Creates a new <see cref="UserFullName"/> instance with the specified first name, middle name, and last name.
        /// </summary>
        /// <param name="firstName">The first name.</param>
        /// <param name="middleName">The middle name.</param>
        /// <param name="lastName">The last name.</param>
        /// <returns>A new <see cref="UserFullName"/> instance.</returns>
        public static UserFullName Create(string? firstName, string? middleName, string? lastName)
        {
            return new(firstName, middleName, lastName);
        }

        /// <summary>
        /// Gets the full name of the user.
        /// </summary>
        /// <returns>The full name of the user.</returns>
        public string GetName()
        {
            return string.Concat(FirstName, " ", string.Concat(MiddleName, " ", LastName).Trim()).Trim();
        }
    }
}

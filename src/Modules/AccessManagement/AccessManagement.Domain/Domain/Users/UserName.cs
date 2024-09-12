namespace Kubis1982.AccessManagement.Domain.Users
{
    using Kubis1982.Modules.AccessManagement.Domain.Users.Exceptions;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Represents a user name.
    /// </summary>
    public sealed partial record UserName
    {
        /// <summary>
        /// The regular expression pattern for a valid user name.
        /// </summary>
        public const string NamePattern = "^[A-Za-z0-9\\.\\-_]+$";

        /// <summary>
        /// Gets the value of the user name.
        /// </summary>
        public string Value { get; private set; } = string.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserName"/> class.
        /// </summary>
        private UserName()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserName"/> class with the specified name.
        /// </summary>
        /// <param name="name">The name to set as the value of the user name.</param>
        /// <exception cref="IncorrectUserNameException">Thrown when the specified name is null, empty, or does not match the name pattern.</exception>
        private UserName(string? name)
        {
            if (string.IsNullOrWhiteSpace(name) || !UserNamePattern().IsMatch(name))
            {
                throw new IncorrectUserNameException(name);
            }
            Value = name ?? string.Empty;
        }

        /// <summary>
        /// Creates a new instance of the <see cref="UserName"/> class with the specified name.
        /// </summary>
        /// <param name="name">The name to set as the value of the user name.</param>
        /// <returns>A new instance of the <see cref="UserName"/> class.</returns>
        public static UserName Of(string? name) => new(name);

        /// <summary>
        /// Implicitly converts a <see cref="UserName"/> object to a string.
        /// </summary>
        /// <param name="argument">The <see cref="UserName"/> object to convert.</param>
        /// <returns>The value of the user name.</returns>
        public static implicit operator string(UserName argument) => argument.Value;

        /// <summary>
        /// Explicitly converts a string to a <see cref="UserName"/> object.
        /// </summary>
        /// <param name="userName">The string to convert.</param>
        /// <returns>A new instance of the <see cref="UserName"/> class with the specified name.</returns>
        public static explicit operator UserName(string userName) => Of(userName);

        /// <summary>
        /// Gets the regular expression pattern for a valid user name.
        /// </summary>
        /// <returns>A <see cref="Regex"/> object representing the user name pattern.</returns>
        [GeneratedRegex(NamePattern)]
        private static partial Regex UserNamePattern();
    }
}

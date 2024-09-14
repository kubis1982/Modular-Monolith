namespace ModularMonolith.Modules.AccessManagement.Domain.Users
{
    using System.Net.Mail;

    /// <summary>
    /// Represents a user email.
    /// </summary>
    public sealed record UserEmail
    {
        /// <summary>
        /// Gets the value of the user email.
        /// </summary>
        public string Value { get; private set; } = string.Empty;

        private UserEmail()
        {
        }

        private UserEmail(string? email)
        {
            if (!MailAddress.TryCreate(email, out _))
            {
                throw new IncorrectEmailException(email);
            }

            Value = email;
        }

        /// <summary>
        /// Creates a new instance of the <see cref="UserEmail"/> class.
        /// </summary>
        /// <param name="email">The email address.</param>
        /// <returns>A new instance of the <see cref="UserEmail"/> class.</returns>
        public static UserEmail Of(string? email)
        {
            return new UserEmail(email);
        }

        /// <summary>
        /// Checks if the specified string is a valid email address.
        /// </summary>
        /// <param name="email">The email address to check.</param>
        /// <returns>True if the specified string is a valid email address; otherwise, false.</returns>
        public static bool IsEmail(string? email)
        {
            return MailAddress.TryCreate(email, out _);
        }

        /// <summary>
        /// Implicitly converts a <see cref="UserEmail"/> object to a string.
        /// </summary>
        /// <param name="argument">The <see cref="UserEmail"/> object to convert.</param>
        /// <returns>The value of the <see cref="UserEmail"/> object.</returns>
        public static implicit operator string(UserEmail argument) => argument.Value;

        /// <summary>
        /// Explicitly converts a string to a <see cref="UserEmail"/> object.
        /// </summary>
        /// <param name="email">The string to convert.</param>
        /// <returns>A new instance of the <see cref="UserEmail"/> class with the specified email address.</returns>
        public static explicit operator UserEmail(string email) => new(email);
    }
}

namespace ModularMonolith.Modules.AccessManagement.Domain.Users
{
    using ModularMonolith.Modules.AccessManagement.Domain;
    using ModularMonolith.Shared.Kernel.Types;

    /// <summary>
    /// Represents the unique identifier for a user.
    /// </summary>
    public sealed record UserId : EntityId<int, EntityType>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserId"/> class with a default value of 0.
        /// </summary>
        public UserId() : this(0)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserId"/> class with the specified identifier.
        /// </summary>
        /// <param name="id">The identifier value.</param>
        public UserId(int id) : base(EntityType.User, id)
        {
        }

        /// <summary>
        /// Gets the administrator user identifier.
        /// </summary>
        public static UserId Administrator => new(1);

        /// <summary>
        /// Implicitly converts an integer value to a <see cref="UserId"/>.
        /// </summary>
        /// <param name="value">The integer value to convert.</param>
        public static explicit operator UserId(int value) => new(value);
    }
}

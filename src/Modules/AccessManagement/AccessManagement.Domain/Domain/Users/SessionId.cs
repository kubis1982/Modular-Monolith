namespace Kubis1982.Modules.AccessManagement.Domain.Users
{
    using Kubis1982.Shared.Kernel.Types;

    /// <summary>
    /// Represents a session identifier.
    /// </summary>
    public sealed record SessionId : EntityId<int, EntityType>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SessionId"/> class.
        /// </summary>
        /// <param name="id">The session identifier.</param>
        public SessionId(int id) : base(EntityType.Session, id)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SessionId"/> class with a default identifier of 0.
        /// </summary>
        public SessionId() : this(0)
        {
        }

        /// <summary>
        /// Defines an explicit conversion from an integer value to a <see cref="SessionId"/>.
        /// </summary>
        /// <param name="value">The integer value to convert.</param>
        /// <returns>A new instance of the <see cref="SessionId"/> class with the specified value.</returns>
        public static explicit operator SessionId(int value) => new(value);
    }
}

namespace Kubis1982.AccessManagement.Domain.Users
{
    using Kubis1982.Shared.Kernel;

    /// <summary>
    /// Represents a base class for user domain events.
    /// </summary>
    public abstract record UserDomainEvent : IDomainEvent
    {
        private readonly User user;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserDomainEvent"/> class.
        /// </summary>
        /// <param name="user">The user associated with the event.</param>
        public UserDomainEvent(User user)
        {
            this.user = user;
        }

        /// <summary>
        /// Gets the ID of the user associated with the event.
        /// </summary>
        public UserId UserId => user.Id;
    }
}

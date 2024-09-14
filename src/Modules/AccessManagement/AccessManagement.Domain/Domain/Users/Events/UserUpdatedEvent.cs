namespace ModularMonolith.Modules.AccessManagement.Domain.Users.Events
{
    using ModularMonolith.Modules.AccessManagement.Domain.Users;

    public sealed record UserUpdatedEvent : UserDomainEvent
    {
        public UserUpdatedEvent(User user, UserFullName userFullName) : base(user)
        {
            UserFullName = userFullName;
        }

        public UserFullName UserFullName { get; }
    }
}

namespace ModularMonolith.Modules.AccessManagement.Domain.Users.Events
{
    using ModularMonolith.Modules.AccessManagement.Domain.Users;

    public sealed record UserDeletedEvent : UserDomainEvent
    {
        public UserDeletedEvent(User user, User currentUser) : base(user)
        {
            CurrentUserId = currentUser.Id;
        }

        public UserId CurrentUserId { get; }
    }
}

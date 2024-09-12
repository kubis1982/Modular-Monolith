namespace Kubis1982.AccessManagement.Domain.Users.Events
{
    public sealed record UserDeletedEvent : UserDomainEvent
    {
        public UserDeletedEvent(User user, User currentUser) : base(user)
        {
            CurrentUserId = currentUser.Id;
        }

        public UserId CurrentUserId { get; }
    }
}

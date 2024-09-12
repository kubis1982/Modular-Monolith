namespace Kubis1982.AccessManagement.Domain.Users.Events
{
    public record UserDeactivatedEvent : UserDomainEvent
    {
        public UserDeactivatedEvent(User user, User currentUser) : base(user)
        {
            CurrentUserId = currentUser.Id;
        }

        public UserId CurrentUserId { get; }
    }
}

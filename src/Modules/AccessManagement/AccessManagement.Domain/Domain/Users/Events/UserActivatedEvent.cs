namespace Kubis1982.AccessManagement.Domain.Users.Events
{
    public record UserActivatedEvent : UserDomainEvent
    {
        public UserActivatedEvent(User user, User currentUser) : base(user)
        {
            CurrentUserId = currentUser.Id;
        }

        public UserId CurrentUserId { get; }
    }
}

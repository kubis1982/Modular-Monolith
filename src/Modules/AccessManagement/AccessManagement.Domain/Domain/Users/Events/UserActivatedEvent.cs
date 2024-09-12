namespace Kubis1982.Modules.AccessManagement.Domain.Users.Events
{
    using Kubis1982.Modules.AccessManagement.Domain.Users;

    public record UserActivatedEvent : UserDomainEvent
    {
        public UserActivatedEvent(User user, User currentUser) : base(user)
        {
            CurrentUserId = currentUser.Id;
        }

        public UserId CurrentUserId { get; }
    }
}

namespace Kubis1982.Modules.AccessManagement.Domain.Users.Events
{
    using Kubis1982.Modules.AccessManagement.Domain.Users;

    public record UserDeactivatedEvent : UserDomainEvent
    {
        public UserDeactivatedEvent(User user, User currentUser) : base(user)
        {
            CurrentUserId = currentUser.Id;
        }

        public UserId CurrentUserId { get; }
    }
}

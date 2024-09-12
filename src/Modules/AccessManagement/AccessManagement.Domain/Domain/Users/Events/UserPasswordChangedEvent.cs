namespace Kubis1982.AccessManagement.Domain.Users.Events
{
    public sealed record UserPasswordChangedEvent : UserDomainEvent
    {
        public UserPasswordChangedEvent(User user) : base(user)
        {
        }
    }
}

namespace Kubis1982.Modules.AccessManagement.Domain.Users.Events
{
    public sealed record UserPasswordChangedEvent : UserDomainEvent
    {
        public UserPasswordChangedEvent(User user) : base(user)
        {
        }
    }
}

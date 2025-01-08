namespace ModularMonolith.Modules.AccessManagement.Domain.Users.Events
{
    public sealed record UserTokenCreatedEvent : UserDomainEvent
    {
        internal UserTokenCreatedEvent(User user, UserToken userToken) : base(user)
        {
            UserToken = userToken;
        }

        public UserToken UserToken { get; }
    }
}

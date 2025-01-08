namespace ModularMonolith.Modules.AccessManagement.Domain.Users.Events
{
    public sealed record UserTokenFinishedEvent : UserDomainEvent
    {
        internal UserTokenFinishedEvent(User user, UserToken userToken) : base(user)
        {
            UserToken = userToken;
        }

        public UserToken UserToken { get; }
    }
}

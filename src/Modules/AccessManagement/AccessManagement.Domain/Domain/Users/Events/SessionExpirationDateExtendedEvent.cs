namespace ModularMonolith.Modules.AccessManagement.Domain.Users.Events
{
    using System;

    public sealed record SessionExpirationDateExtendedEvent : UserDomainEvent
    {
        private readonly Session session;

        public SessionExpirationDateExtendedEvent(User user, Session session, DateTime expirationDate) : base(user)
        {
            this.session = session;
            ExpirationDate = expirationDate;
        }

        public DateTime ExpirationDate { get; }
        public SessionId SessionId => session.Id;
    }
}

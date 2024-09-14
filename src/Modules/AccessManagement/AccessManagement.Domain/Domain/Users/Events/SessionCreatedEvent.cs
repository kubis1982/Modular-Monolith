namespace ModularMonolith.Modules.AccessManagement.Domain.Users.Events
{
    using System;

    public sealed record SessionCreatedEvent : UserDomainEvent
    {
        private readonly Session session;

        public SessionCreatedEvent(User user, Session session, DateTime expiryDate) : base(user)
        {
            this.session = session;
            ExpiryDate = expiryDate;
        }

        public DateTime ExpiryDate { get; }
        public SessionId SessionId => session.Id;
    }
}

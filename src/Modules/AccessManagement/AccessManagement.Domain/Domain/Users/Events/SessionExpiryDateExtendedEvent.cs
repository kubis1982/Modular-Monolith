namespace Kubis1982.Modules.AccessManagement.Domain.Users.Events
{
    using System;

    public sealed record SessionExpiryDateExtendedEvent : UserDomainEvent
    {
        private readonly Session session;

        public SessionExpiryDateExtendedEvent(User user, Session session, DateTime expiryDate) : base(user)
        {
            this.session = session;
            ExpiryDate = expiryDate;
        }

        public DateTime ExpiryDate { get; }
        public SessionId SessionId => session.Id;
    }
}

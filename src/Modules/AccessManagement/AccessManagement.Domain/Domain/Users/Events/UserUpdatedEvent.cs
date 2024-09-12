namespace Kubis1982.AccessManagement.Domain.Users.Events
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public sealed record UserUpdatedEvent : UserDomainEvent
    {
        public UserUpdatedEvent(User user, UserName name, UserFullName userFullName, UserEmail? email) : base(user)
        {
            Name = name;
            UserFullName = userFullName;
            Email = email;
        }

        public UserName Name { get; }
        public UserFullName UserFullName { get; }
        public UserEmail? Email { get; }
    }
}

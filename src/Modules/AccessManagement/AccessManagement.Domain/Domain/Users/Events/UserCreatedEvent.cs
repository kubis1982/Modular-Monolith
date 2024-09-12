namespace Kubis1982.AccessManagement.Domain.Users.Events
{
    public sealed record UserCreatedEvent : UserDomainEvent
    {
        public UserCreatedEvent(User user, UserName name, UserFullName fullName, UserEmail? email) : base(user)
        {
            Name = name;
            FullName = fullName;
            Email = email;
        }

        public UserName Name { get; }
        public UserFullName FullName { get; }
        public UserEmail? Email { get; }
    }
}

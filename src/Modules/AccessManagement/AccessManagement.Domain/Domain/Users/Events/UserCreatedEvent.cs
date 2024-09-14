namespace ModularMonolith.Modules.AccessManagement.Domain.Users.Events
{
    public sealed record UserCreatedEvent : UserDomainEvent
    {
        public UserCreatedEvent(User user, UserEmail email, UserFullName fullName) : base(user)
        {
            Email = email;
            FullName = fullName;            
        }

        public UserEmail? Email { get; }

        public UserFullName FullName { get; }        
    }
}

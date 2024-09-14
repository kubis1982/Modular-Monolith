namespace ModularMonolith.Modules.AccessManagement.Exceptions.Users
{
    using ModularMonolith.Shared.Exceptions;

    public class UserNotFoundException : EntityNotFoundException
    {
        public UserNotFoundException() : base("User")
        {
        }
    }
}

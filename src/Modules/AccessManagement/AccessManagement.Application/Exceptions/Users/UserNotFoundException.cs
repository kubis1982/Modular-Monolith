namespace Kubis1982.Modules.AccessManagement.Exceptions.Users
{
    using Kubis1982.Shared.Exceptions;

    public class UserNotFoundException : EntityNotFoundException
    {
        public UserNotFoundException() : base("User")
        {
        }
    }
}

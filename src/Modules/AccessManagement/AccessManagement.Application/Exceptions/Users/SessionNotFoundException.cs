namespace ModularMonolith.Modules.AccessManagement.Exceptions.Users
{
    using ModularMonolith.Shared.Exceptions;

    public class SessionNotFoundException : EntityNotFoundException
    {
        public SessionNotFoundException() : base("Session")
        {
        }
    }
}

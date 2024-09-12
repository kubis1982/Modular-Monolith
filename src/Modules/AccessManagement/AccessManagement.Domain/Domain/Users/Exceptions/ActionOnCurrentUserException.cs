namespace Kubis1982.AccessManagement.Domain.Users.Exceptions
{
    using Kubis1982.Shared.Exceptions;

    internal class ActionOnCurrentUserException() : AppException("Cannot perform action on the current user.")
    {
    }
}

namespace Kubis1982.Modules.AccessManagement.Domain.Users.Exceptions
{
    using Kubis1982.Shared.Exceptions;

    internal sealed class DeletingAdministratorException() : AppException("Deleting administrator is not allowed.")
    {
    }
}

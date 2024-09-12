namespace Kubis1982.Modules.AccessManagement.Domain.Users.Exceptions
{
    using Kubis1982.Shared.Exceptions;

    public sealed class IncorrectPasswordException() : AppException("Password cannot be empty") { }
}

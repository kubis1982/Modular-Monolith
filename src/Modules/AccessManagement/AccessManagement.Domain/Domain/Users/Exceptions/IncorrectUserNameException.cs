namespace Kubis1982.AccessManagement.Domain.Users.Exceptions
{
    using Kubis1982.Shared.Exceptions;

    public sealed class IncorrectUserNameException(string? userName) : AppException($"User name '{userName}' is incorrect")
    {
    }
}

namespace ModularMonolith.Shared.Exceptions
{
    using System;

    public class AppException(string message) : Exception(message)
    {
    }
}

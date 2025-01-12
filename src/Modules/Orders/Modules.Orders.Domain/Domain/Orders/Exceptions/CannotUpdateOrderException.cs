namespace ModularMonolith.Modules.Orders.Domain.Orders.Exceptions
{
    internal class CannotUpdateOrderException : AppException
    {
        public CannotUpdateOrderException() : base($"Nie można edytować zamówienia, które zostało potwierdzone.")
        {
        }
    }
}
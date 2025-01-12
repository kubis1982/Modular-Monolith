namespace ModularMonolith.Modules.Orders.Domain.Orders.Exceptions
{

    internal class OrderConfirmationWithoutItemsException : AppException
    {
        public OrderConfirmationWithoutItemsException() : base($"Nie można potwierdzić zamówienia bez pozycji")
        {
        }
    }
}

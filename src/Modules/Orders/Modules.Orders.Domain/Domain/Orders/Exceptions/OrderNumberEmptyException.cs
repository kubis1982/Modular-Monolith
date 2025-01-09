namespace ModularMonolith.Modules.Ordering.Domain.Orders.Exceptions
{
    public class OrderNumberEmptyException(string objectName) : FieldEmptyException("OrderNo", objectName)
    {
    }
}
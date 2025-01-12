namespace ModularMonolith.Modules.Orders.Domain.Orders.Exceptions
{
    public class OrderNumberEmptyException(string objectName) : FieldEmptyException("OrderNo", objectName)
    {
    }
}
namespace ModularMonolith.Modules.Ordering.Domain.Orders.Exceptions
{
    using System;

    [Serializable]
    public class WarehouseBlockedException(Warehouse warehouse) : AppException($"Magazyn `({warehouse.Code}) {warehouse.Name}` jest zablokowany. Nie można go wskazać na zamówieniu.")
    {
    }
}
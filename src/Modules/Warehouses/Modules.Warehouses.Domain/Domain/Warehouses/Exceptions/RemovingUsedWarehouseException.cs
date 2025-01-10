namespace ModularMonolith.Modules.Warehouses.Domain.Warehouses.Exceptions
{
    using System;

    [Serializable]
    internal class RemovingUsedWarehouseException(string entityName) : AppException($"Nie można usunąć magazynu. (powiązanie z `{entityName}`)")
    {
    }
}
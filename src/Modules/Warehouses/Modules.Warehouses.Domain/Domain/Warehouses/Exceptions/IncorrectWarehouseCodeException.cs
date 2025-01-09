namespace ModularMonolith.Modules.Warehouses.Domain.Warehouses.Exceptions
{
    public sealed class IncorrectWarehouseCodeException : AppException
    {
        public IncorrectWarehouseCodeException() : base("Kod magazynu nie może być pusty")
        {
        }
    }
}

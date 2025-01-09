namespace ModularMonolith.Modules.Warehouses.Domain.Warehouses.Exceptions
{
    public sealed class IncorrectWarehouseNameException : AppException
    {
        public IncorrectWarehouseNameException() : base("Nazwa magazynu nie może być pusta")
        {
        }
    }
}

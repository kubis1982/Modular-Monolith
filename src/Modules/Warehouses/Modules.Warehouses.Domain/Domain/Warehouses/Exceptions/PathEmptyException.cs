namespace ModularMonolith.Modules.Warehouses.Domain.Warehouses.Exceptions
{

    internal class LocationPathEmptyException : AppException
    {

        public LocationPathEmptyException() : base("Ścieżka nie może być pusta")
        {
        }
    }
}
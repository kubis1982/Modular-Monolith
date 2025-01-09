namespace ModularMonolith.Modules.Warehouses.Domain.Warehouses.Exceptions
{
    internal class PathIncompatibleLtreeRequirementsException : AppException
    {
        public PathIncompatibleLtreeRequirementsException(string path) : base($"Ścieżka {path} jest niezgodna z zasadami hierarchiczności. Nie używaj białych znaków oraz znaków specjalnych.")
        {
        }
    }
}

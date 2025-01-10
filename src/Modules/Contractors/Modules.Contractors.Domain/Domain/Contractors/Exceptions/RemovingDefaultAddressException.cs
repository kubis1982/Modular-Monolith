namespace ModularMonolith.Modules.Contractors.Domain.Contractors.Exceptions
{
    internal class RemovingDefaultAddressException() : AppException($"Domyślnego adresu nie można usunąć")
    {
    }
}
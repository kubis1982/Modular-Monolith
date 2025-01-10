namespace ModularMonolith.Modules.Contractors.Domain.Contractors.Exceptions
{
    internal class AddressNotFoundException(ContractorAddressId addressId) : AppException($"Nie znaleziono adresu o id {addressId.Id}")
    {
    }
}

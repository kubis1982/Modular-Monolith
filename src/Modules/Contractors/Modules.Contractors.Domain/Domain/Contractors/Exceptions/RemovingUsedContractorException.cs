namespace ModularMonolith.Modules.Contractors.Domain.Contractors.Exceptions
{
    internal class RemovingUsedContractorException(string entityName) : AppException($"Nie można usunąć kontrahenta. (powiązanie z `{entityName}`)")
    {
    }
}
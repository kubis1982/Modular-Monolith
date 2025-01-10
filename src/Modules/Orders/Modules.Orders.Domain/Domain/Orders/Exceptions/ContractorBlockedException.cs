namespace ModularMonolith.Modules.Ordering.Domain.Orders.Exceptions
{
    using System;

    [Serializable]
    public class ContractorBlockedException(Contractor contractor) : AppException($"Kontrahent `({contractor.Code}) {contractor.Name}` jest zablokowany. Nie można go wskazać na zamówieniu.")
    {
    }
}
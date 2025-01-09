namespace ModularMonolith.Modules.Contractors.Domain.Contractors.Exceptions
{
    using ModularMonolith.Shared.Exceptions;

    public class ContractorNotFoundException : EntityNotFoundException
    {
        public ContractorNotFoundException(int contractorId) : base($"Kontrahent: {contractorId}")
        {
        }
    }
}

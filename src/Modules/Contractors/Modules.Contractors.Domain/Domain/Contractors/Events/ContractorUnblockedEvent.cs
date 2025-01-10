namespace ModularMonolith.Modules.Contractors.Domain.Contractors.Events
{
    public sealed record ContractorUnblockedEvent : ContractorDomainEvent
    {
        public ContractorUnblockedEvent(Contractor contractor) : base(contractor)
        {
        }
    }
}

namespace ModularMonolith.Modules.Contractors.Domain.Contractors.Events
{
    public sealed record ContractorBlockedEvent : ContractorDomainEvent
    {
        public ContractorBlockedEvent(Contractor contractor) : base(contractor)
        {
        }
    }
}

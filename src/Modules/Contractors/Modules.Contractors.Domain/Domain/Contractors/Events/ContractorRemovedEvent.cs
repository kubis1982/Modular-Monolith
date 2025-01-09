namespace ModularMonolith.Modules.Contractors.Domain.Contractors.Events
{
    public sealed record ContractorRemovedEvent : ContractorDomainEvent
    {
        public ContractorRemovedEvent(Contractor contractor) : base(contractor)
        {
        }
    }
}

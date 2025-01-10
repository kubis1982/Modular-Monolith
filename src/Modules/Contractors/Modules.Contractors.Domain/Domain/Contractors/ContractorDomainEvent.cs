namespace ModularMonolith.Modules.Contractors.Domain.Contractors
{
    public abstract record ContractorDomainEvent : IDomainEvent
    {
        private readonly Contractor contractor;

        public ContractorDomainEvent(Contractor Contractor)
        {
            contractor = Contractor;
        }
        public ContractorId ContractorId => contractor.Id;
    }
}

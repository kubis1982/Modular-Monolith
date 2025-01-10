namespace ModularMonolith.Modules.Contractors.Domain.Contractors.Events
{
    public sealed record ContractorUpdatedEvent : ContractorDomainEvent
    {
        internal ContractorUpdatedEvent(Contractor contractor, ContractorCode code, ContractorName name, string? description, Country country) : base(contractor)
        {
            Code = code;
            Name = name;
            Description = description;
            Country = country;
        }

        public ContractorName Name { get; }
        public ContractorCode Code { get; }
        public string? Description { get; }
        public Country Country { get; }
    }
}

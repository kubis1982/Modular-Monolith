namespace ModularMonolith.Modules.Contractors.Domain.Contractors.Events
{
    public sealed record ContractorCreatedEvent : ContractorDomainEvent
    {
        private readonly ContractorAddress contractorAddress;

        internal ContractorCreatedEvent(Contractor contractor, ContractorName name, ContractorCode code, string? description, Country country, ContractorAddress contractorAddress) : base(contractor)
        {
            this.contractorAddress = contractorAddress;

            Name = name;
            Code = code;
            Description = description;
            Country = country;
            Address = contractorAddress.Address;
        }

        public ContractorName Name { get; }
        public ContractorCode Code { get; }
        public string? Description { get; }
        public Country Country { get; }
        public ContractorAddressId ContractorAddressId => contractorAddress.Id;
        public Address Address { get; }
    }
}

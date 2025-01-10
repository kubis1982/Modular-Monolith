namespace ModularMonolith.Modules.Contractors.Domain.Contractors.Events
{
    using ModularMonolith.Modules.Contractors.Domain.Contractors;

    public sealed record ContractorCreatedAddressEvent : ContractorDomainEvent
    {
        private readonly ContractorAddress contractorAddress;

        internal ContractorCreatedAddressEvent(Contractor Contractor, ContractorAddress contractorAddress) : base(Contractor)
        {
            this.contractorAddress = contractorAddress;

            Address = contractorAddress.Address;
        }

        public Address Address { get; }

        public ContractorAddressId AddressId => contractorAddress.Id;
    }
}

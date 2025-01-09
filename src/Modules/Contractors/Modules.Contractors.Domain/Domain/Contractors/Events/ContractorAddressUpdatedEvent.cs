namespace ModularMonolith.Modules.Contractors.Domain.Contractors.Events
{
    public sealed record ContractorAddressUpdatedEvent : ContractorDomainEvent
    {
        private readonly ContractorAddress contractorAddress;

        internal ContractorAddressUpdatedEvent(Contractor Contractor, ContractorAddress contractorAddress) : base(Contractor)
        {
            this.contractorAddress = contractorAddress;

            Address = contractorAddress.Address;
        }

        public Address Address { get; }

        public ContractorAddressId AddressId => contractorAddress.Id;
    }
}

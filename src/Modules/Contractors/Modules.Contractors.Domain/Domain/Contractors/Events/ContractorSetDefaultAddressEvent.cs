namespace ModularMonolith.Modules.Contractors.Domain.Contractors.Events
{
    using ModularMonolith.Modules.Contractors.Domain.Contractors;
    public sealed record ContractorSetDefaultAddressEvent : ContractorDomainEvent
    {
        public ContractorSetDefaultAddressEvent(Contractor Contractor, ContractorAddressId addressId) : base(Contractor)
        {
            AddressId = addressId;
        }

        public ContractorAddressId AddressId { get; }
    }
}

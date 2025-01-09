namespace ModularMonolith.Modules.Contractors.Domain.Contractors.Events
{
    using ModularMonolith.Modules.Contractors.Domain.Contractors;

    public sealed record ContractorRemovedAddressEvent : ContractorDomainEvent
    {
        public ContractorRemovedAddressEvent(Contractor entity, ContractorAddressId addressId) : base(entity)
        {
            AddressId = addressId;
        }

        public ContractorAddressId AddressId { get; }
    }
}

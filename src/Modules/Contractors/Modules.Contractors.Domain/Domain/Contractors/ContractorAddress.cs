namespace ModularMonolith.Modules.Contractors.Domain.Contractors
{
    using ModularMonolith.Modules.Contractors.Domain;

    public class ContractorAddress : DomainEntity<ContractorAddressId, int, EntityType>
    {
        internal Address Address { get; private set; }
        internal bool IsDefault { get; private set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        private ContractorAddress()
        {
        }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        private ContractorAddress(Address address) : this()
        {
            Address = address;
        }

        internal static ContractorAddress Create(Address address)
        {
            return new ContractorAddress(address);
        }

        internal void Update(Address address)
        {
            Address = address;
        }

        internal void SetDefault()
        {
            IsDefault = true;
        }

        internal void ResetDefault()
        {
            IsDefault = false;
        }
    }
}

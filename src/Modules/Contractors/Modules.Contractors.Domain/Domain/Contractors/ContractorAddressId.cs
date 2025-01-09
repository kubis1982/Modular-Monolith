namespace ModularMonolith.Modules.Contractors.Domain.Contractors
{
    using ModularMonolith.Modules.Contractors.Domain;

    public sealed record ContractorAddressId : EntityId<int, EntityType>
    {
        public ContractorAddressId() : this(0)
        {
        }

        public ContractorAddressId(int id) : base(EntityType.Address, id)
        {
        }

        public static implicit operator ContractorAddressId(int value) => new(value);
    }
}

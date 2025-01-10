namespace ModularMonolith.Modules.Contractors.Domain.Contractors
{

    public sealed record ContractorId : EntityId<int, EntityType>
    {
        public ContractorId() : this(0)
        {
        }

        public ContractorId(int id) : base(EntityType.Contractor, id)
        {
        }

        public static implicit operator ContractorId(int value) => new(value);
        public static implicit operator int(ContractorId value) => value.Id;
    }
}

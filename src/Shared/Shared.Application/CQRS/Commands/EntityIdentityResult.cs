namespace ModularMonolith.Shared.CQRS.Commands
{
    using ModularMonolith.Shared.Kernel.Types;

    public class EntityIdentityResult
    {
        private EntityIdentityResult(EntityTypeId typeId, int id)
        {
            TypeId = typeId;
            Id = id;
        }

        public static EntityIdentityResult Create<TEntityId, TEntityTypeEnumerator>(DomainEntity<TEntityId, int, TEntityTypeEnumerator> d)
            where TEntityId : EntityId<int, TEntityTypeEnumerator>
            where TEntityTypeEnumerator : EntityTypeEnumerator
        {
            return new EntityIdentityResult(d.Id.TypeId, d.Id.Id);
        }

        public EntityTypeId TypeId { get; }
        public int Id { get; }

        public static EntityIdentityResult Empty => new( EntityTypeId.Empty, 0);
    }
}

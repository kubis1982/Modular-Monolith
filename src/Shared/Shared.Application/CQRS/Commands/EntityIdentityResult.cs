namespace ModularMonolith.Shared.CQRS.Commands
{
    using ModularMonolith.Shared.Kernel.Types;
    using System;

    public class EntityIdentityResult
    {
        private Func<EntityTypeId> entityTypeIdValue;
        private Func<int> entityIdValue;

        private EntityIdentityResult(Func<EntityTypeId> entityTypeIdValue, Func<int> entityIdValue)
        {
            this.entityTypeIdValue = entityTypeIdValue;
            this.entityIdValue = entityIdValue;
        }

        public static EntityIdentityResult Create<TEntityId, TEntityTypeEnumerator>(DomainEntity<TEntityId, int, TEntityTypeEnumerator> d)
            where TEntityId : EntityId<int, TEntityTypeEnumerator>
            where TEntityTypeEnumerator : EntityTypeEnumerator
        {
            return new EntityIdentityResult(() => d.Id.TypeId, () => d.Id.Id);
        }

        public EntityTypeId TypeId => entityTypeIdValue.Invoke();
        public int Id => entityIdValue.Invoke();

        public static EntityIdentityResult Empty => new(() => EntityTypeId.Empty, () => 0);
    }
}

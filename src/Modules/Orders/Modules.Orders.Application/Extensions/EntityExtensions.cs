namespace ModularMonolith.Modules.Orders.Extensions
{
    using ModularMonolith.Modules.Orders.Domain;
    using ModularMonolith.Shared.Kernel.Types;

    internal static class EntityExtensions
    {
        public static EntityIdentityResult CreateEntityIdentityResult<TEntityId>(this DomainEntity<TEntityId, int> domainEntity) where TEntityId : EntityId<int>
        {
            EntityType entityType = EntityType.GetEntityType(domainEntity.GetType());
            return new(() => entityType.Code, () => domainEntity.Id.Id);
        }
    }
}

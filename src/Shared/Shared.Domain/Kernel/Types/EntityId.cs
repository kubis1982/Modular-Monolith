namespace ModularMonolith.Shared.Kernel.Types
{
    public record EntityId<TKey, TEntityTypeEnumerator> : EntityId<TKey> where TEntityTypeEnumerator : EntityTypeEnumerator
    {
        public EntityId(TEntityTypeEnumerator entityTypeEnumerator, TKey id) : base(id)
        {
            TypeId = entityTypeEnumerator.Code;
        }

        public EntityTypeId TypeId { get; }
    }

    public record EntityId<TKey>(TKey Id);

    public record EntityId(EntityTypeId TypeId, int Id) : EntityId<int>(Id)
    {
        public static EntityId Empty => new(EntityTypeId.Empty, 0);

        public static bool TryCreate(string? typeId, int? id, out EntityId entityId)
        {
            entityId = Empty;

            if ((typeId ?? "") == "" && (id ?? 0) == 0)
            {
                return false;
            }

            try
            {
                entityId = new EntityId(typeId, id ?? 0);
                return true;
            }
            catch
            {
                entityId = Empty;
                return false;
            }
        }
    }
}

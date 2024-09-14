namespace ModularMonolith.Shared.Persistance.WriteModel
{
    using ModularMonolith.Shared.Kernel;
    using ModularMonolith.Shared.Kernel.Types;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public abstract class EntityTypeConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : class
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.ToTable($"{typeof(TEntity).Name}s");
            OnConfigure(builder);
        }

        protected abstract void OnConfigure(EntityTypeBuilder<TEntity> builder);
    }

    public abstract class EntityTypeConfiguration<TEntity, TKey> : EntityTypeConfiguration<TEntity>
         where TEntity : class, IDomainEntity
    {
    }

    public abstract class EntityTypeConfiguration<TEntity, TEntityId, TKey, TEntityTypeEnumerator> : EntityTypeConfiguration<TEntity, TKey>
        where TEntity : DomainEntity<TEntityId, TKey, TEntityTypeEnumerator>
        where TEntityId : EntityId<TKey, TEntityTypeEnumerator>
        where TEntityTypeEnumerator : EntityTypeEnumerator
    {
    }
}

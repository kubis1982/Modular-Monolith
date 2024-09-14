namespace ModularMonolith.Shared.Persistance.WriteModel
{
    using ModularMonolith.Shared.Kernel.Types;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using System;

    public abstract class DomainEntityTypeConfiguration<TEntity, TEntityId, TEntityTypeEnumerator> : EntityTypeConfiguration<TEntity, TEntityId, int, TEntityTypeEnumerator>
        where TEntity : DomainEntity<TEntityId, int, TEntityTypeEnumerator>
        where TEntityId : EntityId<int, TEntityTypeEnumerator>, new()
        where TEntityTypeEnumerator : EntityTypeEnumerator
    {
        public override void Configure(EntityTypeBuilder<TEntity> builder)
        {
            base.Configure(builder);
            TEntityId entityId = new() { Value = 0 };
            builder.Property<EntityTypeId>("TypeId").HasColumnOrder(0).HasDefaultValue(entityId.TypeId).IsRequired(true);
            AddIdentityField(builder);
            AddCreatedField(builder);
            AddModifiedField(builder);
            AddKey(builder);
        }

        public virtual void AddIdentityField(EntityTypeBuilder<TEntity> builder)
        {
            builder.Property(n => n.Id).IsRequired(true)
                .HasConversion(n => n.Value, n => new TEntityId { Value = n })
                .ValueGeneratedOnAdd()
                .HasColumnOrder(1);
        }

        public virtual void AddCreatedField(EntityTypeBuilder<TEntity> builder)
        {
            builder.Property<DateTime?>(ShadowProperties.CreatedOn).HasColumnOrder(2)
                .ValueGeneratedOnAdd().HasDefaultValueSql("NOW()");
            builder.Property<int?>(ShadowProperties.CreatedBy).HasColumnOrder(3);
        }

        public virtual void AddModifiedField(EntityTypeBuilder<TEntity> builder)
        {
            builder.Property<DateTime?>(ShadowProperties.ModifiedOn).HasColumnOrder(4);
            builder.Property<int?>(ShadowProperties.ModifiedBy).HasColumnOrder(5);
        }

        public virtual void AddKey(EntityTypeBuilder<TEntity> builder)
        {
            builder.HasKey(n => n.Id);
        }
    }
}

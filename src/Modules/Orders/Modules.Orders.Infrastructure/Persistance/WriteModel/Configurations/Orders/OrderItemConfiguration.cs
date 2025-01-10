namespace ModularMonolith.Modules.Ordering.Persistance.WriteModel.Configurations.Orders
{
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using ModularMonolith.Modules.Ordering.Domain.Orders;
    using ModularMonolith.Shared.Kernel.Types;
    using System;

    internal class OrderItemConfiguration : EntityTypeConfiguration<OrderItem>
    {
        protected override void OnConfigure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.Property<EntityTypeId>("TypeId").HasColumnOrder(1).IsRequired(true);

            builder.HasDiscriminator<EntityTypeId>("TypeId")
                .HasValue<PurchaseOrderItem>(EntityType.PurchaseOrderItem.Code)
                .HasValue<SalesOrderItem>(EntityType.SalesOrderItem.Code);

            builder.Property(n => n.Id).IsRequired(true)
                .HasConversion(n => n.Id, n => n)
                .ValueGeneratedOnAdd()
                .HasColumnOrder(2);

            builder.Property<DateTime?>(ShadowProperties.CreatedOn).HasColumnOrder(3)
               .ValueGeneratedOnAdd().HasDefaultValueSql("NOW()");
            builder.Property<int?>(ShadowProperties.CreatedBy).HasColumnOrder(4);

            builder.Property<DateTime?>(ShadowProperties.ModifiedOn).HasColumnOrder(5);
            builder.Property<int?>(ShadowProperties.ModifiedBy).HasColumnOrder(6);

            builder.Property<Guid?>(ShadowProperties.Version).IsConcurrencyToken(true).HasColumnOrder(7);

            builder.Property(n => n.Position);

            builder.OwnsOne(n => n.ArticleId, n =>
            {
                n.Property(n => n.TypeId).HasColumnName("ArticleTypeId");
                n.Property(n => n.Id).HasColumnName("ArticleId");
            });
            builder.OwnsOne(n => n.Quantity, n =>
            {
                n.Property(n => n.Value).HasColumnName("Quantity");
                n.Property(n => n.Unit).HasColumnName("QuantityUnit").HasMaxLength(OrderRestriction.UnitLength);
                n.Property(n => n.Numerator).HasColumnName("QuantityNumerator");
                n.Property(n => n.Denominator).HasColumnName("QuantityDenominator");
            });

            builder.Property(n => n.QuantityCompleted);

            builder.Property(n => n.Description).IsRequired(false).HasMaxLength(OrderRestriction.DescriptionLength);

            builder.HasKey(n => n.Id);

            builder.HasIndex(n => new { n.Id, n.Position });
        }
    }
}

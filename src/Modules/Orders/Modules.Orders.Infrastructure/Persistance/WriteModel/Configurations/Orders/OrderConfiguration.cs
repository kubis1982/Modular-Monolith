namespace ModularMonolith.Modules.Ordering.Persistance.WriteModel.Configurations.Orders
{
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using ModularMonolith.Modules.Ordering.Domain.Orders;
    using ModularMonolith.Shared.Kernel.Types;
    using System;

    internal class OrderConfiguration : EntityTypeConfiguration<Order>
    {
        protected override void OnConfigure(EntityTypeBuilder<Order> builder)
        {
            builder.Property<EntityTypeId>("TypeId").HasColumnOrder(1).IsRequired(true);

            builder.HasDiscriminator<EntityTypeId>("TypeId")
               .HasValue<SalesOrder>(EntityType.SalesOrder.Code)
               .HasValue<PurchaseOrder>(EntityType.PurchaseOrder.Code);

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

            builder.Property(n => n.OrderType).HasConversion(n => n.Key, n => Enumeration<byte>.FromValue<OrderType>(n));
            builder.Property(n => n.OrderNo).HasMaxLength(OrderRestriction.OrderNumberLength).HasConversion(n => n.Value, n => OrderNumber.Of(n));
            builder.Property(n => n.ExecutionDate);

            builder.Property(n => n.Status).HasConversion(n => n.Key, n => Enumeration<byte>.FromValue<OrderStatus>(n));

            builder.OwnsOne(n => n.ContractorId, n =>
            {
                n.Property(n => n.TypeId).HasColumnName("ContractorTypeId");
                n.Property(n => n.Id).HasColumnName("ContractorId");
            });

            builder.OwnsOne(n => n.WarehouseId, n =>
            {
                n.Property(n => n.TypeId).HasColumnName("WarehouseTypeId");
                n.Property(n => n.Id).HasColumnName("WarehouseId");
            });

            builder.Property(n => n.Description).IsRequired(false).HasMaxLength(OrderRestriction.DescriptionLength);

            builder.OwnsOne(n => n.Address, n =>
            {
                n.Property(n => n.Line1).HasMaxLength(OrderRestriction.AddressLineLength).HasColumnName("AddressLine1");
                n.Property(n => n.Line2).HasMaxLength(OrderRestriction.AddressLineLength).HasColumnName("AddressLine2");
                n.Property(n => n.City).HasMaxLength(OrderRestriction.AddressCityLength).HasColumnName("AddressCity");
                n.Property(n => n.PostalCode).HasMaxLength(OrderRestriction.AddressPostalCodeLength).HasColumnName("AddressPostalCode");
                n.Property(n => n.Country).HasMaxLength(OrderRestriction.AddressCountryLength).IsFixedLength().HasColumnName("AddressCountry");
            });

            builder.HasMany(n => n.Items).WithOne(n => n.Order).HasForeignKey("OrderId");

            builder.Navigation(n => n.Items).AutoInclude(false);

            builder.HasKey(n => n.Id);
        }
    }
}

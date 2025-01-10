namespace ModularMonolith.Modules.Warehouses.Persistance.WriteModel.Configurations.Warehouses
{
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using ModularMonolith.Modules.Warehouses.Persistance;

    internal class WarehouseConfiguration : DomainEntityTypeConfiguration<Warehouse, WarehouseId, EntityType>
    {
        protected override void OnConfigure(EntityTypeBuilder<Warehouse> builder)
        {
            builder.Property(n => n.Code).HasConversion(x => x.Value, y => WarehouseCode.Of(y)).HasMaxLength(WarehouseRestriction.CodeLength);
            builder.Property(n => n.Name).HasConversion(x => x.Value, y => WarehouseName.Of(y)).HasMaxLength(WarehouseRestriction.NameLength);
            builder.Property(n => n.Description).HasMaxLength(WarehouseRestriction.DescriptionLength);
            builder.Property(n => n.IsBlocked);
            builder.HasIndex(n => n.Code).IsUnique();
        }
    }
}

namespace ModularMonolith.Modules.Articles.Persistance.WriteModel.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class MeasurementUnitConfiguration : DomainEntityTypeConfiguration<MeasurementUnit, MeasurementUnitId, EntityType>
    {
        protected override void OnConfigure(EntityTypeBuilder<MeasurementUnit> builder)
        {
            builder.Property(n => n.Code).HasConversion(n => n.Value, n => MeasurementUnitCode.Of(n)).HasMaxLength(MeasurementUnitRestriction.CodeLength).HasColumnOrder(100);
            builder.Property(n => n.Name).HasConversion(n => n.Value, n => MeasurementUnitName.Of(n)).HasMaxLength(MeasurementUnitRestriction.NameLength).HasColumnOrder(101);

            builder.HasIndex(n => n.Code).IsUnique();
        }
    }
}

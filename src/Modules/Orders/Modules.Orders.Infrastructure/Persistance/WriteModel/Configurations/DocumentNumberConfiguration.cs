namespace ModularMonolith.Modules.Orders.Persistance.WriteModel.Configurations
{
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class DocumentNumberConfiguration : EntityTypeConfiguration<DocumentNumber>
    {

        protected override void OnConfigure(EntityTypeBuilder<DocumentNumber> builder)
        {
            builder.HasKey(n => new { n.EntityTypeId, n.Year, n.Month, n.Day });
        }
    }
}

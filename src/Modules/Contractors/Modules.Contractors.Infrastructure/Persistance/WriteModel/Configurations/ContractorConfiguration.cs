namespace ModularMonolith.Modules.Contractors.Persistance.WriteModel.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class ContractorConfiguration : DomainEntityTypeConfiguration<Contractor, ContractorId, EntityType>
    {

        protected override void OnConfigure(EntityTypeBuilder<Contractor> builder)
        {
            builder.Property(n => n.Code).HasConversion(n => n.Value, n => ContractorCode.Of(n)).HasMaxLength(ContractorRestriction.CodeLength).HasColumnOrder(10);
            builder.Property(n => n.Name).HasConversion(n => n.Value, n => ContractorName.Of(n)).HasMaxLength(ContractorRestriction.NameLength).HasColumnOrder(11);
            builder.Property(n => n.Description).HasMaxLength(ContractorRestriction.DescriptionLength).HasColumnOrder(12);
            builder.Property(n => n.IsBlocked).HasColumnOrder(13);
            builder.OwnsOne(n => n.Country, n =>
            {
                n.Property(m => m.Value).HasMaxLength(ContractorRestriction.CountryLength).HasColumnName("Country");
            });

            builder.HasIndex(n => n.Code).IsUnique();

            builder.HasMany(n => n.Addresses).WithOne().HasForeignKey("ContractorId").IsRequired(true);

            builder.Navigation(n => n.Addresses).AutoInclude(false);


        }
    }
}

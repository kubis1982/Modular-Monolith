namespace ModularMonolith.Modules.Contractors.Persistance.WriteModel.Configurations
{
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using ModularMonolith.Modules.Contractors.Domain.Contractors;
    using ModularMonolith.Shared.Kernel.Types;

    internal class ContractorAddressConfiguration : DomainEntityTypeConfiguration<ContractorAddress, ContractorAddressId, EntityType>
    {
        protected override void OnConfigure(EntityTypeBuilder<ContractorAddress> builder)
        {
            builder.Property<EntityTypeId>("ContractorTypeId").HasDefaultValue(EntityType.Contractor.Code).HasColumnOrder(10);
            builder.Property<ContractorId>("ContractorId").HasColumnOrder(11);

            builder.OwnsOne(n => n.Address, n =>
            {
                n.Property(m => m.Line1).HasMaxLength(ContractorRestriction.AddressLineLength).HasColumnName("Line1");
                n.Property(m => m.Line2).HasMaxLength(ContractorRestriction.AddressLineLength).HasColumnName("Line2");
                n.Property(m => m.PostalCode).HasMaxLength(ContractorRestriction.AddressPostalCodeLength).HasColumnName("PostalCode");

                n.OwnsOne(m => m.City, m =>
                {
                    m.Property(p => p.Value).HasMaxLength(ContractorRestriction.AddressCityLength).HasColumnName("City");
                });

                n.OwnsOne(m => m.Country, m =>
                {
                    m.Property(p => p.Value).HasMaxLength(ContractorRestriction.AddressCountryLength).HasColumnName("Country");
                });
            });

            builder.Property(n => n.IsDefault);

            builder.ToTable("Addresses");
        }
    }
}

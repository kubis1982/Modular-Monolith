namespace ModularMonolith.Modules.Orders.Domain.Tests
{
    using AutoFixture;
    using ModularMonolith.Shared.Kernel.Types;

    internal class Customization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.Customize<Contractor>(n => n.FromFactory(() => new Contractor(fixture.Create<ContractorId>(), fixture.Create<string>(), fixture.Create<string>(), false)).OmitAutoProperties());
            fixture.Customize<Warehouse>(n => n.FromFactory(() => new Warehouse(fixture.Create<WarehouseId>(), fixture.Create<string>(), fixture.Create<string>(), false)).OmitAutoProperties());
            fixture.Customize<Article>(n => n.FromFactory(() => new Article(fixture.Create<ArticleId>(), fixture.Create<string>(), fixture.Create<string>(), "kg", false)).OmitAutoProperties());
            fixture.Customize<EntityTypeId>(n => n.FromFactory(() => "Exa01"));
            fixture.Customize<Address>(n => n.FromFactory(() => Address.Create(fixture.Create<string>(), fixture.Create<string>(), "63-900", "Rawicz", "PL")));
        }
    }
}

namespace ModularMonolith.Modules.Warehouses.Domain.Warehouses
{
    using AutoFixture;

    internal class WarehouseCustomisation : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.Customize<Warehouse>(n => n.FromFactory(() =>
              Warehouse.Create(fixture.Create<WarehouseCode>(), fixture.Create<string>(), fixture.Create<string>())
                  .Extensions().SetValue(n => n.Id, new WarehouseId(fixture.Create<int>())).DomainEntity));

            fixture.Customize<WarehouseCode>(n => n.FromFactory(() =>
            WarehouseCode.Of((string)fixture.Create(
                new RegularExpressionRequest(WarehouseCode.Pattern),
                new SpecimenContext(fixture)))));
        }
    }
}

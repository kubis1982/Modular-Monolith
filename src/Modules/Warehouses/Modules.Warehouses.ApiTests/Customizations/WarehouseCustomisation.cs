namespace ModularMonolith.Modules.Warehouses.Customizations
{
    using AutoFixture;
    using AutoFixture.Kernel;
    using ModularMonolith.Modules.Warehouses.Domain;
    using ModularMonolith.Modules.Warehouses.Persistance;
    using ModularMonolith.Modules.Warehouses.Persistance.ReadModel.Entities;
    using ModularMonolith.Modules.Warehouses.Requests.Warehouses;

    internal class WarehouseCustomisation : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.Customize<WarehouseEntity>(n => n
                .With(n => n.TypeId, EntityType.Warehouse.Code.Value)
                .With(n => n.IsBlocked, false)
                .With(n => n.Id, () => fixture.Create(
                new RangedNumberRequest(typeof(int), 10, int.MaxValue),
                new SpecimenContext(fixture)))
                .With(n => n.Code, () => new SpecimenContext(fixture).Resolve(new RegularExpressionRequest(WarehouseRestriction.CodePattern)))
                );

            fixture.Customize<CreateWarehouseRequest>(n => n
            .With(m => m.Code, (string)fixture.Create(
                new RegularExpressionRequest(WarehouseRestriction.CodePattern),
                new SpecimenContext(fixture))));

            fixture.Customize<UpdateWarehouseRequest>(n => n
            .With(m => m.Code, (string)fixture.Create(
                new RegularExpressionRequest(WarehouseRestriction.CodePattern),
                new SpecimenContext(fixture))));
        }
    }
}

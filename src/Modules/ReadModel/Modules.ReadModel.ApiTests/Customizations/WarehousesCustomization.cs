namespace ModularMonolith.Modules.ReadModel.Customizations {
    using AutoFixture;
    using Microsoft.EntityFrameworkCore;
    using ModularMonolith.Modules.ReadModel.Persistance.ReadModel.Warehouses;

    internal class WarehousesCustomization : ICustomization {
        public void Customize(IFixture fixture) {
            fixture.Customize<WarehouseEntity>(n => n
                .With(m => m.IsBlocked, false));
        }
    }
}

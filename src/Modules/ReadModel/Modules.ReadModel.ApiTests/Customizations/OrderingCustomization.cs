namespace ModularMonolith.Modules.ReadModel.Customizations {
    using AutoFixture;
    using ModularMonolith.Modules.ReadModel.Persistance.ReadModel.Ordering;

    internal class OrderingCustomization : ICustomization {
        public void Customize(IFixture fixture) {
            fixture.Customize<OrderEntity>(n => n
                .With(m => m.Status, 0));
        }
    }
}

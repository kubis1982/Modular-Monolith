namespace ModularMonolith.Shared.Fixtures.Customizations {
    using AutoFixture;
    using System.Text.Json;

    internal class JsonElementCustomisation : ICustomization {
        public void Customize(IFixture fixture) {
            fixture.Customize<JsonElement>(n => n.FromFactory(() => JsonDocument.Parse("{}").RootElement));
        }
    }
}

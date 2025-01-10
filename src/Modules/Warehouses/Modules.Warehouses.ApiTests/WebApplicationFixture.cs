namespace ModularMonolith.Modules.Warehouses
{
    using ModularMonolith.Modules.Warehouses.Persistance.ReadModel;

    [CollectionDefinition(nameof(WebApplicationFixtureCollection))]
    public class WebApplicationFixtureCollection : ICollectionFixture<WebApplicationFixture> { }

    public class WebApplicationFixture() : Shared.Api.WebApplicationFixture<ReadDbContext>(ModuleDefinition.MODULE_CODE)
    {
    }
}
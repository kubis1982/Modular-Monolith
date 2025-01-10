namespace ModularMonolith.Modules.Contractors
{
    using ModularMonolith.Modules.Contractors.Persistance.ReadModel;

    [CollectionDefinition(nameof(WebApplicationFixtureCollection))]
    public class WebApplicationFixtureCollection : ICollectionFixture<WebApplicationFixture> { }

    public class WebApplicationFixture() : Shared.Api.WebApplicationFixture<ReadDbContext>(ModuleDefinition.MODULE_CODE)
    {
    }
}
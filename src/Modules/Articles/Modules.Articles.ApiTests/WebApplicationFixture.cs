namespace ModularMonolith.Modules.Articles
{
    using ModularMonolith.Modules.Articles.Persistance.ReadModel;

    [CollectionDefinition(nameof(WebApplicationFixtureCollection))]
    public class WebApplicationFixtureCollection : ICollectionFixture<WebApplicationFixture> { }

    public class WebApplicationFixture() : Shared.Api.WebApplicationFixture<ReadDbContext>(ModuleDefinition.MODULE_CODE)
    {
    }
}
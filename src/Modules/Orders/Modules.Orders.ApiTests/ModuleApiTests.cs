namespace ModularMonolith.Modules.Ordering
{
    using ModularMonolith.Modules.Ordering.Persistance.ReadModel;
    using ModularMonolith.Shared;
    using Xunit.Abstractions;

    [Collection(nameof(WebApplicationFixtureCollection))]
    public class ModuleApiTests(WebApplicationFixture webApplicationFixture, ITestOutputHelper testOutputHelper) : ApiTests<ReadDbContext>(webApplicationFixture, testOutputHelper)
    {
    }
}

namespace ModularMonolith.Modules.Orders
{
    using ModularMonolith.Modules.Orders.Persistance.ReadModel;
    using ModularMonolith.Shared;
    using Xunit.Abstractions;

    [Collection(nameof(WebApplicationFixtureCollection))]
    public class ModuleApiTests(WebApplicationFixture webApplicationFixture, ITestOutputHelper testOutputHelper) : ApiTests<ReadDbContext>(webApplicationFixture, testOutputHelper)
    {
    }
}

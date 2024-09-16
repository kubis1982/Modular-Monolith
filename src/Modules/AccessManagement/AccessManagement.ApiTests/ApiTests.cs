namespace ModularMonolith.Modules.AccessManagement
{
    using ModularMonolith.Shared;
    using Xunit.Abstractions;

    public class ApiTests(ITestOutputHelper testOutputHelper) : ApiTests<TestHelperFactory>(testOutputHelper)
    {
    }
}

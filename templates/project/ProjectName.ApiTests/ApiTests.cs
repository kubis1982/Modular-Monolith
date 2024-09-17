namespace ModularMonolith.Modules.ProjectName
{
    using Microsoft.Extensions.DependencyInjection;
    using ModularMonolith.Shared;
    using Xunit.Abstractions;

    public class ApiTests(ITestOutputHelper testOutputHelper) : ApiTests<TestHelperFactory>(testOutputHelper)
    {
    }
}

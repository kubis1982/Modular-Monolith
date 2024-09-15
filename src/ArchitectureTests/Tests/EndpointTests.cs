namespace ModularMonolith.ArchitectureTests.Tests
{
    using ModularMonolith.ArchitectureTests.Extensions;
    using ModularMonolith.Shared.Modules;
    using NetArchTest.Rules;

    public class EndpointTests : ArchitectureTests
    {
        [Fact]
        public void EndpointsClassesShouldHaveNameEndingWithEndpoints() => Types.InAssemblies(AssemblyHelper.GetAssemblies())
                .That().ImplementInterface(typeof(IModuleEndpoints)).Should()
                .HaveNameEndingWith("Endpoints").GetResult()
                .ShouldBeTrue();
    }
}

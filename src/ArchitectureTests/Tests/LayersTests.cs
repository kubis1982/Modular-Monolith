using ModularMonolith.ArchitectureTests.Extensions;
using NetArchTest.Rules;

namespace ModularMonolith.ArchitectureTests.Tests
{
    public class LayersTests : ArchitectureTests {
        [Fact]
        public void DomainLayerShouldNotHaveDependencyToApplicationLayer() => Types.InAssemblies(AssemblyHelper.GetDomainAssemblies())
                .Should()
                .NotHaveDependencyOnAll(AssemblyHelper.GetApplicationAssemblies().Select(n => n.FullName).ToArray())
                .GetResult()
                .ShouldBeTrue();

        [Fact]
        public void ApplicationLayerShouldNotHaveDependencyToInfrastructureLayer() => Types.InAssemblies(AssemblyHelper.GetApplicationAssemblies())
                .Should()
                .NotHaveDependencyOnAll(AssemblyHelper.GetInfrastructureAssemblies().Select(n => n.FullName).ToArray())
                .GetResult()
                .ShouldBeTrue();

        [Fact]
        public void DomainLayerShouldNotHaveDependencyToInfrastructureLayer() => Types.InAssemblies(AssemblyHelper.GetDomainAssemblies())
                .Should()
                .NotHaveDependencyOnAll(AssemblyHelper.GetInfrastructureAssemblies().Select(n => n.FullName).ToArray())
                .GetResult()
                .ShouldBeTrue();
    }
}

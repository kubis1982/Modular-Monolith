using Kubis1982.ArchitectureTests.Extensions;
using NetArchTest.Rules;

namespace Kubis1982.ArchitectureTests.Tests
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

namespace ModularMonolith.ArchitectureTests.Tests
{
    using ModularMonolith.ArchitectureTests.Extensions;
    using ModularMonolith.Shared.Exceptions;
    using ModularMonolith.Shared.Kernel;
    using NetArchTest.Rules;

    public class DomainTests : ArchitectureTests
    {
        [Fact]
        public void ExceptionClassesShouldHaveNameEndingWithException() => Types.InAssemblies(AssemblyHelper.GetAssemblies())
                .That().Inherit(typeof(AppException)).Should()
                .HaveNameEndingWith("Exception").GetResult()
                .ShouldBeTrue();

        [Fact]
        public void DomainEventsShouldHaveNameEndingWithEventAndBeImmutable() => Types.InAssemblies(AssemblyHelper.GetAssemblies())
                .That().ImplementInterface(typeof(IDomainEvent)).Should()
                .HaveNameEndingWith("Event").And().BeImmutable().GetResult()
                .ShouldBeTrue();

        [Fact]
        public void DomainEventsShouldNotHavePropertiesThatReferToDomainEvent()
        {
            var domainEventTypes = Types.InAssemblies(AssemblyHelper.GetAssemblies())
                .That().ImplementInterface(typeof(IDomainEvent)).GetTypes();

            var failingTypes = new List<Type>();
            foreach (var domainEventType in domainEventTypes)
            {
                var properties = domainEventType.GetProperties();
                foreach (var property in properties)
                {
                    if (property.PropertyType.IsSubclassOf(typeof(IDomainEntity)) == true)
                    {
                        failingTypes.Add(domainEventType);
                        break;
                    }
                }
            }

            failingTypes.Should().BeEmpty(string.Join(Environment.NewLine, failingTypes.Select(n => n.FullName)));
        }

        [Fact]
        public void DomainEntitiesShouldBeImmutable() => Types.InAssemblies(AssemblyHelper.GetAssemblies())
                .That().ImplementInterface(typeof(IDomainEntity)).Should()
                .BeImmutable().GetResult()
                .ShouldBeTrue();
    }
}

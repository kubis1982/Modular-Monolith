namespace ModularMonolith.ArchitectureTests.Tests
{
    using FluentValidation;
    using ModularMonolith.ArchitectureTests.Extensions;
    using ModularMonolith.Shared.CQRS.Commands;
    using ModularMonolith.Shared.CQRS.Queries;
    using NetArchTest.Rules;

    public class CQRSTests : ArchitectureTests {
        [Fact]
        public void CommandClassesShouldHaveNameEndingWithCommand() => Types.InAssemblies(AssemblyHelper.GetAssemblies())
                .That().Inherit(typeof(ICommand<>)).And().AreNotAbstract().Or().Inherit(typeof(ICommand)).And().AreNotAbstract().Should()
                .HaveNameEndingWith("Command").GetResult()
                .ShouldBeTrue();

        [Fact]
        public void CommandHandlerClassesShouldHaveNameEndingWithCommandHandler() => Types.InAssemblies(AssemblyHelper.GetAssemblies())
                .That().Inherit(typeof(CommandHandler<>)).And().AreNotAbstract().Or().Inherit(typeof(CommandHandler<,>)).And().AreNotAbstract().Should()
                .HaveNameEndingWith("CommandHandler").GetResult()
                .ShouldBeTrue();

        [Fact]
        public void ValidatorClassesShouldHaveNameEndingWithValidator() => Types.InAssemblies(AssemblyHelper.GetAssemblies())
                .That().Inherit(typeof(AbstractValidator<>)).And().AreNotAbstract().Should()
                .HaveNameEndingWith("Validator").GetResult()
                .ShouldBeTrue();

        [Fact]
        public void QueryClassesShouldHaveNameEndingWithQuery() => Types.InAssemblies(AssemblyHelper.GetAssemblies())
                 .That().Inherit(typeof(Query<>)).Should()
                 .HaveNameEndingWith("Query").GetResult()
                 .ShouldBeTrue();

        [Fact]
        public void QueryHandlerClassesShouldHaveNameEndingWithQueryHandler() => Types.InAssemblies(AssemblyHelper.GetAssemblies())
                 .That().Inherit(typeof(QueryHandler<,>)).And().AreNotAbstract().Should()
                 .HaveNameEndingWith("QueryHandler").GetResult()
                 .ShouldBeTrue();
    }
}

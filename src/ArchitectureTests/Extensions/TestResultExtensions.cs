namespace ModularMonolith.ArchitectureTests.Extensions
{
    internal static class TestResultExtensions
    {
        public static void ShouldBeTrue(this NetArchTest.Rules.TestResult @object)
        {
            @object.IsSuccessful.Should().BeTrue(@object.IsSuccessful ? "" : string.Join(Environment.NewLine, @object.FailingTypeNames));
        }
    }
}

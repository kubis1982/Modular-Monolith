namespace ModularMonolith.Shared
{
    using AutoFixture.Xunit2;
    using ModularMonolith.Shared.Fixtures;

    public class InlineDataFixtureAttribute(params object[] values) : InlineAutoDataAttribute(new AutoDataFixtureAttribute(), values)
    {
        private class AutoDataFixtureAttribute : AutoDataAttribute
        {
            public AutoDataFixtureAttribute()
              : base(FixtureFactory.Create)
            {
            }
        }
    }
}

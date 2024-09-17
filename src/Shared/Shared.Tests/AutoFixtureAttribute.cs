namespace ModularMonolith.Shared
{
    using AutoFixture;
    using AutoFixture.AutoMoq;
    using AutoFixture.Xunit2;
    using ModularMonolith.Shared.Extensions;

    /// <summary>
    /// Custom attribute for AutoFixture data generation.
    /// </summary>
    public class AutoFixtureAttribute(params object[] values) : InlineAutoDataAttribute(new AutoFixtureDataAttribute(), values)
    {
        private class AutoFixtureDataAttribute : AutoDataAttribute
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="AutoFixtureDataAttribute"/> class.
            /// </summary>
            public AutoFixtureDataAttribute()
              : base(() =>
              {
                  var fixture = new Fixture().Customize(new AutoMoqCustomization());

                  AppDomain.CurrentDomain.GetSystemAssemblies().SelectMany(x => x.GetTypes())
                   .Where(n => typeof(ICustomization).IsAssignableFrom(n) && n.IsInterface == false && n.IsAbstract == false)
                       .Select(n => (ICustomization)Activator.CreateInstance(n)!)
                       .ToList().ForEach(n =>
                       {
                           fixture.Customize(n);
                       });

                  fixture.RepeatCount = 5;
                  fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList().ForEach(b => fixture.Behaviors.Remove(b));
                  fixture.Behaviors.Add(new OmitOnRecursionBehavior());
                  return fixture;
              })
            {
            }
        }
    }
}

namespace ModularMonolith.Shared.Fixtures {
    using AutoFixture;
    using AutoFixture.AutoMoq;
    using System;
    using System.Linq;

    internal class FixtureFactory {
        static FixtureFactory() {
            customizations = AppDomain.CurrentDomain.GetAssemblies().Where(n => n.FullName?.StartsWith("ModularMonolith") == true).SelectMany(x => x.GetTypes())
                 .Where(n => typeof(ICustomization).IsAssignableFrom(n) && n.IsInterface == false && n.IsAbstract == false)
                     .Select(n => (ICustomization)Activator.CreateInstance(n)!).ToList();
        }

        private static readonly IList<ICustomization> customizations;

        public static IFixture Create() {
            var fixture = new Fixture()
                .Customize(new AutoMoqCustomization());
            customizations.ToList().ForEach(n => {
                fixture.Customize(n);
            });
            fixture.RepeatCount = 5;
            fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList().ForEach(b => fixture.Behaviors.Remove(b));
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            return fixture;
        }
    }
}

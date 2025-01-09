namespace ModularMonolith.Modules.Ordering.Domain.Orders.Tests
{
    internal class OrderAutoFixture<T>(params object[] values) : InlineDataFixtureAttribute([(OrderFactory)Activator.CreateInstance(typeof(T))!, .. values]) where T : OrderFactory
    {
    }
}

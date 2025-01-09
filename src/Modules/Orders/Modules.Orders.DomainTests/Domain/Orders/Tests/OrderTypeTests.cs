namespace ModularMonolith.Modules.Ordering.Domain.Orders.Tests
{
    public class OrderTypeTests : ModuleDomainTests
    {
        [Theory]
        [MemberData(nameof(OrderTypes))]
        public void ShouldGetOrderType(Type type, OrderType orderType)
        {
            // Act
            OrderType currentOrderType = OrderType.GetOrderType(type);

            // Assert
            currentOrderType.Should().Be(orderType);
        }

        public static IEnumerable<object[]> OrderTypes()
        {
            yield return new object[] { typeof(PurchaseOrder), OrderType.Purchase };
            yield return new object[] { typeof(SalesOrder), OrderType.Sale };
        }
    }
}
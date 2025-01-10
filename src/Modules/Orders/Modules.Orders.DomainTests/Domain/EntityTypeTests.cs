using ModularMonolith.Modules.Ordering.Domain.Orders;

namespace ModularMonolith.Modules.Ordering.Domain.Tests
{
    public class EntityTypeTests : ModuleDomainTests
    {
        [Theory]
        [MemberData(nameof(OrderTypes))]
        public void ShouldGetEntityType(Type type, EntityType entityType)
        {
            // Act
            EntityType currentEntityType = EntityType.GetEntityType(type);

            // Assert
            currentEntityType.Should().Be(entityType);
        }

        public static IEnumerable<object[]> OrderTypes()
        {
            yield return new object[] { typeof(PurchaseOrder), EntityType.PurchaseOrder };
            yield return new object[] { typeof(PurchaseOrderItem), EntityType.PurchaseOrderItem };
            yield return new object[] { typeof(SalesOrder), EntityType.SalesOrder };
            yield return new object[] { typeof(SalesOrderItem), EntityType.SalesOrderItem };
        }
    }
}

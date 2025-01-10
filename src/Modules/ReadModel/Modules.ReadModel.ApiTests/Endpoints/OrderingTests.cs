using ModularMonolith.Modules.ReadModel.Persistance.ReadModel.Ordering;
using ModularMonolith.Modules.ReadModel.Queries.Ordering;
using ModularMonolith.Shared;
using Xunit.Abstractions;

namespace ModularMonolith.Modules.ReadModel.Endpoints {

    public class OrderingTests(WebApplicationFixture webApplicationFixture, ITestOutputHelper testOutputHelper) : ModuleApiTests(webApplicationFixture, testOutputHelper) {
        [Theory]
        [InlineDataFixture(OrderType.Sales)]
        [InlineDataFixture(OrderType.Purchase)]
        public async Task ShouldGetOrder(OrderType orderType, OrderEntity orderEntity) {
            // Arrange
            orderEntity.OrderType = (byte)orderType;

            DbContext.Add(orderEntity);
            DbContext.SaveChanges();

            // Act
            var result = await HttpClient.GetAsync<GetOrderQueryResult>($"/orm/{orderType.ToString().ToLower()}-orders/{orderEntity.Id}");

            // Assert
            result.Should().NotBeNull();
        }

        [Theory]
        [InlineDataFixture(OrderType.Sales)]
        [InlineDataFixture(OrderType.Purchase)]
        public async Task ShouldGetOrderItem(OrderType orderType, OrderItemEntity orderItemEntity) {
            // Arrange
            orderItemEntity.Order.OrderType = (byte)orderType;

            DbContext.Add(orderItemEntity);
            DbContext.SaveChanges();

            // Act
            var result = await HttpClient.GetAsync<GetOrderItemQueryResult>($"/orm/{orderType.ToString().ToLower()}-orders/{orderItemEntity.Order.Id}/items/{orderItemEntity.Id}");

            // Assert
            result.Should().NotBeNull();
        }
    }
}

using ModularMonolith.Modules.Ordering.Domain.Orders;
using ModularMonolith.Modules.Ordering.Persistance.ReadModel;
using ModularMonolith.Modules.Ordering.Requests.Orders;
using Xunit.Abstractions;

namespace ModularMonolith.Modules.Ordering.Endpoints.Orders.Tests
{
    public class OrdersTests(WebApplicationFixture webApplicationFixture, ITestOutputHelper testOutputHelper) : ModuleApiTests(webApplicationFixture, testOutputHelper)
    {
        private static string GetOrderName(Persistance.ReadModel.OrderType orderType) => orderType == Persistance.ReadModel.OrderType.Purchase ? "purchase" : "sales";

        private static string GetTypeId(Persistance.ReadModel.OrderType orderType) => orderType == Persistance.ReadModel.OrderType.Purchase ? Domain.EntityType.PurchaseOrder.Code : Domain.EntityType.SalesOrder.Code;
        private static string GetTypeIdForItem(Persistance.ReadModel.OrderType orderType) => orderType == Persistance.ReadModel.OrderType.Purchase ? Domain.EntityType.PurchaseOrderItem.Code : Domain.EntityType.SalesOrderItem.Code;

        [Theory]
        [InlineDataFixture(Persistance.ReadModel.OrderType.Sale, "ZS")]
        [InlineDataFixture(Persistance.ReadModel.OrderType.Purchase, "ZZ")]
        public async Task ShouldCreateOrder(Persistance.ReadModel.OrderType orderType, string prefix, CreatePurchaseOrderRequest request)
        {
            // Act
            var identity = await HttpClient.PostAndReturnIdentityAsync($"/{GetOrderName(orderType)}-orders", request);

            // Assert
            var result = DbContext.Orders.Single(n => n.Id == identity.Id);
            result.WarehouseId.Should().Be(request.WarehouseId);
            result.ContractorId.Should().Be(request.ContractorId);
            result.ExecutionDate.Should().NotBeNull();
            result.Status.Should().Be((byte)OrderStatus.Unconfirmed.Key);
            result.Description.Should().Be(request.Description);
            result.OrderNo.Should().Be($"{prefix}-{result.CreatedOn!.Value.Year}/{result.CreatedOn!.Value.Month:D2}/{result.CreatedOn!.Value.Day:D2}/00001");
            result.AddressLine1.Should().Be(request.Address!.Line1);
            result.AddressLine2.Should().Be(request.Address!.Line2);
            result.AddressPostalCode.Should().Be(request.Address!.PostalCode);
            result.AddressCity.Should().Be(request.Address!.City);
            result.AddressCountry.Should().Be(request.Address!.Country);
        }

        [Theory]
        [InlineDataFixture(Persistance.ReadModel.OrderType.Sale, "ZS")]
        [InlineDataFixture(Persistance.ReadModel.OrderType.Purchase, "ZZ")]
        public async Task ShouldCreateManyOrdersWithOrderNo(Persistance.ReadModel.OrderType orderType, string prefix, CreatePurchaseOrderRequest request1, CreatePurchaseOrderRequest request2
  , CreatePurchaseOrderRequest request3, CreatePurchaseOrderRequest request4, CreatePurchaseOrderRequest request5, CreatePurchaseOrderRequest request6)
        {
            // Act
            var identity1 = await HttpClient.PostAndReturnIdentityAsync($"/{GetOrderName(orderType)}-orders", request1);
            var identity2 = await HttpClient.PostAndReturnIdentityAsync($"/{GetOrderName(orderType)}-orders", request2);
            var identity3 = await HttpClient.PostAndReturnIdentityAsync($"/{GetOrderName(orderType)}-orders", request3);
            var identity4 = await HttpClient.PostAndReturnIdentityAsync($"/{GetOrderName(orderType)}-orders", request4);
            var identity5 = await HttpClient.PostAndReturnIdentityAsync($"/{GetOrderName(orderType)}-orders", request5);
            var identity6 = await HttpClient.PostAndReturnIdentityAsync($"/{GetOrderName(orderType)}-orders", request6);

            // Assert
            var result1 = DbContext.Orders.Single(n => n.Id == identity1.Id);
            result1.OrderNo.Should().Be($"{prefix}-{result1.CreatedOn!.Value.Year}/{result1.CreatedOn!.Value.Month:D2}/{result1.CreatedOn!.Value.Day:D2}/00001");
            var result2 = DbContext.Orders.Single(n => n.Id == identity2.Id);
            result2.OrderNo.Should().Be($"{prefix}-{result2.CreatedOn!.Value.Year}/{result1.CreatedOn!.Value.Month:D2}/{result1.CreatedOn!.Value.Day:D2}/00002");
            var result3 = DbContext.Orders.Single(n => n.Id == identity3.Id);
            result3.OrderNo.Should().Be($"{prefix}-{result1.CreatedOn!.Value.Year}/{result1.CreatedOn!.Value.Month:D2}/{result1.CreatedOn!.Value.Day:D2}/00003");
            var result4 = DbContext.Orders.Single(n => n.Id == identity4.Id);
            result4.OrderNo.Should().Be($"{prefix}-{result1.CreatedOn!.Value.Year}/{result1.CreatedOn!.Value.Month:D2}/{result1.CreatedOn!.Value.Day:D2}/00004");
            var result5 = DbContext.Orders.Single(n => n.Id == identity5.Id);
            result5.OrderNo.Should().Be($"{prefix}-{result1.CreatedOn!.Value.Year}/{result1.CreatedOn!.Value.Month:D2}/{result1.CreatedOn!.Value.Day:D2}/00005");
            var result6 = DbContext.Orders.Single(n => n.Id == identity6.Id);
            result6.OrderNo.Should().Be($"{prefix}-{result1.CreatedOn!.Value.Year}/{result1.CreatedOn!.Value.Month:D2}/{result1.CreatedOn!.Value.Day:D2}/00006");
        }

        [Theory]
        [InlineDataFixture(Persistance.ReadModel.OrderType.Sale)]
        [InlineDataFixture(Persistance.ReadModel.OrderType.Purchase)]
        public async Task ShouldUpdateOrder(Persistance.ReadModel.OrderType orderType, OrderEntity order, UpdatePurchaseOrderRequest request)
        {
            // Arrange
            order.OrderType = (byte)orderType;
            order.TypeId = GetTypeId(orderType);
            DbContext.Add(order);
            DbContext.SaveChanges();

            // Act
            await HttpClient.PutAsync($"/{GetOrderName(orderType)}-orders/{order.Id}", request);

            // Assert
            var result = DbContext.Orders.Single(n => n.Id == order.Id);
            result.WarehouseId.Should().Be(request.WarehouseId);
            result.ContractorId.Should().Be(request.ContractorId);
            result.Description.Should().Be(request.Description);
            result.ExecutionDate?.ToLongTimeString().Should().Be(request.ExecutionDate.ToLongTimeString());
            result.Status.Should().Be(order.Status);
            result.AddressLine1.Should().Be(request.Address!.Line1);
            result.AddressLine2.Should().Be(request.Address!.Line2);
            result.AddressPostalCode.Should().Be(request.Address!.PostalCode);
            result.AddressCity.Should().Be(request.Address!.City);
            result.AddressCountry.Should().Be(request.Address!.Country);
        }

        [Theory]
        [InlineDataFixture(Persistance.ReadModel.OrderType.Sale)]
        [InlineDataFixture(Persistance.ReadModel.OrderType.Purchase)]
        public async Task ShouldDeleteOrder(Persistance.ReadModel.OrderType orderType, OrderEntity order)
        {
            // Arrange
            order.OrderType = (byte)orderType;
            order.TypeId = GetTypeId(orderType);
            DbContext.Add(order);
            DbContext.SaveChanges();

            // Act
            await HttpClient.DeleteAndEnsureNoContentAsync($"/{GetOrderName(orderType)}-orders/{order.Id}");

            // Assert
            DbContext.Orders.Any(n => n.Id == order.Id).Should().BeFalse();
        }

        [Theory]
        [InlineDataFixture(Persistance.ReadModel.OrderType.Sale)]
        [InlineDataFixture(Persistance.ReadModel.OrderType.Purchase)]
        public async Task ShouldAddOrderItem(Persistance.ReadModel.OrderType orderType, OrderEntity order, OrderItemEntity item1, CreatePurchaseItemRequest request)
        {
            // Arrange
            order.OrderType = (byte)orderType;
            order.TypeId = GetTypeId(orderType);
            DbContext.Add(order);
            DbContext.SaveChanges();
            item1.OrderId = order.Id;
            item1.Position = 1;
            DbContext.Add(item1);
            DbContext.SaveChanges();

            // Act
            var identity = await HttpClient.PostAndReturnIdentityAsync($"/{GetOrderName(orderType)}-orders/{order.Id}/items", request);

            // Assert
            var result = DbContext.OrderItems.Single(n => n.Id == identity.Id);
            result.ArticleId.Should().Be(request.ArticleId);
            result.Quantity.Should().Be(request.Quantity);
            result.QuantityUnit.Should().Be(request.Unit);
            result.QuantityNumerator.Should().Be(request.Numerator);
            result.QuantityDenominator.Should().Be(request.Denominator);
            result.Description.Should().Be(request.Description);
            result.Position.Should().Be(2);
        }

        [Theory]
        [InlineDataFixture(Persistance.ReadModel.OrderType.Sale)]
        [InlineDataFixture(Persistance.ReadModel.OrderType.Purchase)]
        public async Task ShouldUpdateOrderItem(Persistance.ReadModel.OrderType orderType, int orderId, OrderEntity order, OrderItemEntity item, UpdatePurchaseItemRequest request)
        {
            // Arrange
            order.OrderType = (byte)orderType;
            order.TypeId = GetTypeId(orderType);
            order.Id = orderId;
            DbContext.Add(order);
            DbContext.SaveChanges();
            item.OrderId = orderId;
            DbContext.Add(item);
            DbContext.SaveChanges();

            // Act
            await HttpClient.PutAsync($"/{GetOrderName(orderType)}-orders/{order.Id}/items/{item.Id}", request);

            // Assert
            var result = DbContext.OrderItems.Single(n => n.Id == item.Id);
            result.ArticleId.Should().Be(request.ArticleId);
            result.Quantity.Should().Be(request.Quantity);
            result.QuantityUnit.Should().Be(request.Unit);
            result.QuantityNumerator.Should().Be(request.Numerator);
            result.QuantityDenominator.Should().Be(request.Denominator);
            result.Description.Should().Be(request.Description);
        }

        [Theory]
        [InlineDataFixture(Persistance.ReadModel.OrderType.Sale)]
        [InlineDataFixture(Persistance.ReadModel.OrderType.Purchase)]
        public async Task ShouldDeleteOrderItem(Persistance.ReadModel.OrderType orderType, int orderId, OrderEntity order, OrderItemEntity item1, OrderItemEntity item2, OrderItemEntity item3)
        {
            // Arrange
            order.OrderType = (byte)orderType;
            order.TypeId = GetTypeId(orderType);
            order.Id = orderId;
            DbContext.Add(order);
            DbContext.SaveChanges();
            item1.OrderId = orderId;
            item1.Position = 1;
            DbContext.Add(item1);
            DbContext.SaveChanges();
            item2.OrderId = orderId;
            item2.Position = 2;
            DbContext.Add(item2);
            DbContext.SaveChanges();
            item3.OrderId = orderId;
            item3.Position = 3;
            DbContext.Add(item3);
            DbContext.SaveChanges();

            // Act
            await HttpClient.DeleteAndEnsureNoContentAsync($"/{GetOrderName(orderType)}-orders/{order.Id}/items/{item2.Id}");

            // Assert
            var array = DbContext.OrderItems.Where(n => n.OrderId == order.Id).ToArray();
            array.Should().HaveCount(2);
            array.Single(n => n.Id == item1.Id).Position.Should().Be(1);
            array.Single(n => n.Id == item3.Id).Position.Should().Be(2);
        }


        [Theory]
        [InlineDataFixture(Persistance.ReadModel.OrderType.Sale)]
        [InlineDataFixture(Persistance.ReadModel.OrderType.Purchase)]
        public async Task ShouldConfirmOrder(Persistance.ReadModel.OrderType orderType, int orderId, OrderEntity order, OrderItemEntity item)
        {
            // Arrange
            order.OrderType = (byte)orderType;
            order.TypeId = GetTypeId(orderType);
            order.Id = orderId;
            DbContext.Add(order);
            DbContext.SaveChanges();
            item.OrderId = orderId;
            DbContext.Add(item);
            DbContext.SaveChanges();

            // Act
            await HttpClient.PatchAsync($"/{GetOrderName(orderType)}-orders/{order.Id}/confirm");

            // Assert
            var result = DbContext.Orders.Single(n => n.Id == order.Id);
            result.Status.Should().Be((byte)OrderStatus.Confirmed.Key);
        }
    }
}

namespace ModularMonolith.Modules.Ordering.Domain.Orders.Tests
{
    using ModularMonolith.Modules.Ordering.Domain.Orders.Events;
    using ModularMonolith.Modules.Ordering.Domain.Orders.Exceptions;
    using ModularMonolith.Shared.Kernel.Types;

    public class OrderTests : ModuleDomainTests
    {
        [Theory]
        [OrderAutoFixture<PurchaseOrderFactory>]
        [OrderAutoFixture<SalesOrderFactory>]
        public void ShouldCreateOrder(OrderFactory orderTest, Contractor contractor, Warehouse warehouse, DateTime dateTime, string orderNo, string? description, Address address)
        {
            // Arrange
            contractor = contractor with { IsBlocked = false };
            warehouse = warehouse with { IsBlocked = false };

            // Act
            Orders.Order order = orderTest.Create(contractor, warehouse, dateTime, orderNo, description, address);

            // Assert

            order.Status.Should().Be(Orders.OrderStatus.Unconfirmed);

            var @event = order.Extensions().GetEvent<OrderCreatedEvent>();
            @event.ContractorId.Should().Be(order.ContractorId);
            @event.OrderId.Should().Be(order.Id);
            @event.ExecutionDate.Should().Be(order.ExecutionDate);
            @event.State.Should().Be(order.Status);
            @event.OrderNo.Should().Be(order.OrderNo);
            @event.Description.Should().Be(description);
            @event.Address.Should().BeEquivalentTo(order.Address);
        }

        [Theory]
        [OrderAutoFixture<PurchaseOrderFactory>]
        [OrderAutoFixture<SalesOrderFactory>]
        public void ShouldNotCreateOrderIfContractorIsBlocked(OrderFactory orderTest, Contractor contractor, Warehouse warehouse, DateTime dateTime, string orderNo, string? description, Address address)
        {
            // Arrange
            contractor = contractor with { IsBlocked = true };

            // Act
            Action action = () => orderTest.Create(contractor, warehouse, dateTime, orderNo, description, address);

            // Assert
            action.Should().Throw<ContractorBlockedException>();
        }

        [Theory]
        [OrderAutoFixture<PurchaseOrderFactory>]
        [OrderAutoFixture<SalesOrderFactory>]
        public void ShouldNotCreateOrderIfWarehouseIsBlocked(OrderFactory orderTest, Contractor contractor, Warehouse warehouse, DateTime dateTime, string orderNo, string? description, Address address)
        {
            // Arrange
            warehouse = warehouse with { IsBlocked = true };

            // Act
            Action action = () => orderTest.Create(contractor, warehouse, dateTime, orderNo, description, address);

            // Assert
            action.Should().Throw<WarehouseBlockedException>();
        }

        [Theory]
        [InlineDataFixture]
        public void ShouldUpdateOrder(Orders.Order order, Contractor contractor, Warehouse warehouse, DateTime executionDate, string? description, Address address)
        {
            // Act
            order.Update(contractor, warehouse, executionDate, description, address);

            // Assert
            order.Address.Should().BeEquivalentTo(address);

            var @event = order.Extensions().GetEvent<OrderUpdatedEvent>();
            @event.ContractorId.Should().Be(order.ContractorId);
            @event.WarehouseId.Should().Be(order.WarehouseId);
            @event.OrderId.Should().Be(order.Id);
            @event.ExecutionDate.Should().Be(order.ExecutionDate);
            @event.Description.Should().Be(order.Description);
            @event.Address.Should().BeEquivalentTo(order.Address);
        }

        [Theory]
        [InlineDataFixture]
        public void ShouldntUpdateOrderWhenConfirmed(Orders.Order order, Contractor contractor, Warehouse warehouse, DateTime executionDate, OrderItem orderItem, string? description, Address address)
        {
            order.Extensions().SetValue(n => n.Items, new List<OrderItem>() { orderItem });
            order.Confirm();

            Action action = () => order.Update(contractor, warehouse, executionDate, description, address);
            action.Should().ThrowExactly<CannotUpdateOrderException>();
        }

        [Theory]
        [InlineDataFixture]
        public void ShouldConfirmOrder(Orders.Order order, OrderItem item)
        {
            order.Extensions().SetValue(n => n.Items, new List<OrderItem>() { item });
            order.Confirm();

            var @event = order.Extensions().GetEvent<OrderConfirmedEvent>();
            @event.OrderId.Should().Be(order.Id);
        }

        [Theory]
        [InlineDataFixture]
        public void ShouldRemoveOrder(Orders.Order order)
        {
            order.Remove();

            var @event = order.Extensions().GetEvent<OrderRemovedEvent>();
            @event.OrderId.Should().Be(order.Id);
        }

        [Theory]
        [InlineDataFixture]
        public void ShouldntRemoveConfirmedOrder(Orders.Order order, OrderItem orderItem)
        {
            order.Extensions().SetValue(n => n.Items, new List<OrderItem>() { orderItem });
            order.Confirm();

            Action action = () => order.Remove();
            action.Should().ThrowExactly<CannotUpdateOrderException>();
        }

        [Theory]
        [InlineDataFixture]
        public void ShouldAddItem(Orders.Order order, Article article, Quantity quantity, string? description)
        {
            // Act
            order.AddItem(article, quantity, description);

            // Assert
            var @event = order.Extensions().GetEvent<OrderItemCreatedEvent>();
            @event.ArticleId.Should().Be(article.Id);
            @event.Quantity.Should().Be(quantity);
            @event.OrderId.Should().Be(order.Id);
            @event.Description.Should().Be(description);
        }

        [Theory]
        [InlineDataFixture]
        public void ShouldNotAddItemIfArticleIsBlocked(Orders.Order order, Article article, Quantity quantity, string? description)
        {
            // Arrange
            article = article with { IsBlocked = true };

            // Act
            Action action = () => order.AddItem(article, quantity, description);

            // Assert
            action.Should().Throw<ArticleBlockedException>();
        }

        [Theory]
        [InlineDataFixture(OrderStatus.Confirmed)]
        [InlineDataFixture(OrderStatus.InProgress)]
        [InlineDataFixture(OrderStatus.Completed)]
        [InlineDataFixture(OrderStatus.Finished)]
        public void ShouldntAddItemToConfirmedOrder(OrderStatus orderStatus, Order order, Article article, Quantity quantity, string? description)
        {
            // Arrange
            order.Extensions().SetValue(n => n.Status, Enumeration<byte>.FromValue<Orders.OrderStatus>((byte)orderStatus));

            // Act
            Action action = () => order.AddItem(article, quantity, description);

            // Assert
            action.Should().Throw<CannotUpdateOrderException>();
        }

        [Theory]
        [InlineDataFixture]
        public void ShouldRemoveItem(Orders.Order order, OrderItem orderItem)
        {
            order.Extensions().SetValue(n => n.Items, new List<OrderItem>() { orderItem });
            orderItem.Extensions().SetValue(n => n.Order, order);
            order.RemoveItem(orderItem.Id);

            var @event = order.Extensions().GetEvent<OrderItemRemovedEvent>();
            @event.OrderId.Should().Be(order.Id);
        }
        [Theory]
        [InlineDataFixture(OrderStatus.Confirmed)]
        [InlineDataFixture(OrderStatus.InProgress)]
        [InlineDataFixture(OrderStatus.Completed)]
        [InlineDataFixture(OrderStatus.Finished)]
        public void ShouldNotRemoveItemIfOrderIsNotUnconfirmed(OrderStatus orderStatus, Order order)
        {
            // Arrange
            order.Extensions().SetValue(n => n.Status, Enumeration<byte>.FromValue<Orders.OrderStatus>((byte)orderStatus));

            // Act
            Action action = () => order.RemoveItem(2);

            // Assert
            action.Should().Throw<CannotUpdateOrderException>();
        }

        [Theory]
        [InlineDataFixture]
        public void ShouldUpdateItem(Orders.Order order, OrderItem orderItem, Article article, Quantity quantity, string? description)
        {
            order.Extensions().SetValue(n => n.Items, new List<OrderItem>() { orderItem });

            order.UpdateItem(orderItem.Id, article, quantity, description);

            var @event = order.Extensions().GetEvent<OrderItemUpdatedEvent>();
            @event.ArticleId.Should().Be(article.Id);
            @event.Quantity.Should().Be(quantity);
            @event.OrderItemId.Should().Be(order.Id);
            @event.Description.Should().Be(description);
        }

        [Theory]
        [InlineDataFixture(OrderStatus.Confirmed)]
        [InlineDataFixture(OrderStatus.InProgress)]
        [InlineDataFixture(OrderStatus.Completed)]
        [InlineDataFixture(OrderStatus.Finished)]
        public void ShouldNotUpdateItemIfOrderIsNotUnconfirmed(OrderStatus orderStatus, Order order, Article article, Quantity quantity, string? description)
        {
            // Arrange
            order.Extensions().SetValue(n => n.Status, Enumeration<byte>.FromValue<Orders.OrderStatus>((byte)orderStatus));

            // Act
            Action action = () => order.UpdateItem(2, article, quantity, description);

            // Assert
            action.Should().Throw<CannotUpdateOrderException>();
        }

        [Theory]
        [InlineDataFixture]
        public void ShouldntConfirmOrderWithoutItems(Orders.Order order)
        {
            Action action = () => order.Confirm();
            action.Should().ThrowExactly<OrderConfirmationWithoutItemsException>();
        }
    }
}

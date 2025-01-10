namespace ModularMonolith.Modules.Warehouses.Domain.Warehouses
{

    public class WarehouseTests : ModuleDomainTests
    {
        [Theory]
        [InlineDataFixture]
        public void ShouldCreateWarehouse(Warehouse warehouse)
        {
            var @event = warehouse.Extensions().GetEvent<Events.WarehouseCreatedEvent>();
            @event.WarehouseId.Should().Be(warehouse.Id);
            @event.Code.Should().Be(warehouse.Code);
            @event.Name.Should().Be(warehouse.Name);
            @event.Description.Should().Be(warehouse.Description);
        }

        [Theory]
        [InlineDataFixture]
        public void ShouldUpdateWarehouse(Warehouse warehouse, WarehouseCode newCode, WarehouseName newName, string description)
        {
            warehouse.Update(newCode, newName, description);

            var @event = warehouse.Extensions().GetEvent<Events.WarehouseUpdatedEvent>();
            @event.WarehouseId.Should().Be(warehouse.Id);
            @event.Code.Should().Be(newCode);
            @event.Name.Should().Be(newName);
            @event.Description.Should().Be(description);
        }

        [Theory]
        [InlineDataFixture]
        public void ShouldRemoveWarehouse(Warehouse warehouse)
        {
            // Arrange
            Mock<IWarehouseUsageService> warehouseUsageService = new Mock<IWarehouseUsageService>();

            // Act
            warehouse.Remove(warehouseUsageService.Object);

            // Assert
            var @event = warehouse.Extensions().GetEvent<Events.WarehouseRemovedEvent>();
            @event.WarehouseId.Should().Be(warehouse.Id);
        }

        [Theory]
        [InlineDataFixture]
        public void ShouldUnblockWarehouse(Warehouse warehouse)
        {
            warehouse.Extensions().SetValue(n => n.IsBlocked, true);
            warehouse.Unblock();
            var @event = warehouse.Extensions().GetEvent<Events.WarehouseUnblockedEvent>();
            @event.WarehouseId.Should().Be(warehouse.Id);
        }

        [Theory]
        [InlineDataFixture]
        public void ShouldBlockWarehouse(Warehouse warehouse)
        {
            warehouse.Block();

            var @event = warehouse.Extensions().GetEvent<Events.WarehouseBlockedEvent>();
            @event.WarehouseId.Should().Be(warehouse.Id);
        }
    }
}

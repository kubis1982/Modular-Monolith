namespace ModularMonolith.Modules.Warehouses.Endpoints
{
    using ModularMonolith.Modules.Warehouses.Persistance.ReadModel.Entities;
    using ModularMonolith.Modules.Warehouses.Queries.Warehoues;
    using ModularMonolith.Modules.Warehouses.Requests.Warehouses;
    using ModularMonolith.Shared;
    using System.Threading.Tasks;
    using Xunit.Abstractions;

    public class WarehousesTests(WebApplicationFixture webApplicationFixture, ITestOutputHelper testOutputHelper) : ModuleApiTests(webApplicationFixture, testOutputHelper)
    {
        [Theory]
        [InlineDataFixture]
        public async Task ShouldCreateWarehouse(CreateWarehouseRequest request)
        {
            // Act
            var warehouseIdentity = await HttpClient.PostAndReturnIdentityAsync($"/warehouses", request);

            // Assert
            var result = DbContext.Warehouses.Single(n => n.Id == warehouseIdentity.Id);
            result.Name.Should().Be(request.Name);
            result.Code.Should().Be(request.Code);
            result.Description.Should().Be(request.Description);
            result.IsBlocked.Should().BeFalse();
        }

        [Theory]
        [InlineDataFixture]
        public async Task ShouldUpdateWarehouse(WarehouseEntity warehouseEntity, UpdateWarehouseRequest request)
        {
            // Arrange
            DbContext.Add(warehouseEntity);
            DbContext.SaveChanges();

            // Act
            await HttpClient.PutAsync($"/warehouses/{warehouseEntity.Id}", request);

            // Assert
            var result = DbContext.Warehouses.Single(n => n.Id == warehouseEntity.Id);
            result.Name.Should().Be(request.Name);
            result.Code.Should().Be(request.Code);
            result.Description.Should().Be(request.Description);
            result.IsBlocked.Should().BeFalse();
        }

        [Theory]
        [InlineDataFixture]
        public async Task ShouldDeleteWarehouse(WarehouseEntity warehouseEntity)
        {
            // Arrange
            DbContext.Add(warehouseEntity);
            DbContext.SaveChanges();

            // Act
            await HttpClient.DeleteAndEnsureNoContentAsync($"/warehouses/{warehouseEntity.Id}");

            // Assert
            DbContext.Warehouses.SingleOrDefault(n => n.Id == warehouseEntity.Id).Should().BeNull();
        }

        [Theory]
        [InlineDataFixture]
        public async Task ShouldBlockWarehouse(WarehouseEntity warehouseEntity)
        {
            // Arrange
            warehouseEntity.IsBlocked = false;
            DbContext.Add(warehouseEntity);
            DbContext.SaveChanges();

            // Act
            await HttpClient.PatchAsync($"/warehouses/{warehouseEntity.Id}/block");

            // Assert
            DbContext.Warehouses.Single(n => n.Id == warehouseEntity.Id).IsBlocked.Should().BeTrue();
        }

        [Theory]
        [InlineDataFixture]
        public async Task ShouldUnblockWarehouse(WarehouseEntity warehouseEntity)
        {
            // Arrange
            warehouseEntity.IsBlocked = true;
            DbContext.Add(warehouseEntity);
            DbContext.SaveChanges();

            // Act
            await HttpClient.PatchAsync($"/warehouses/{warehouseEntity.Id}/unblock");

            // Assert
            DbContext.Warehouses.Single(n => n.Id == warehouseEntity.Id).IsBlocked.Should().BeFalse();
        }

        [Theory]
        [InlineDataFixture]
        public async Task ShouldGetWarehouse(WarehouseEntity warehouseEntity)
        {
            // Arrange
            DbContext.Add(warehouseEntity);
            DbContext.SaveChanges();

            // Act
            var result = await HttpClient.GetAsync<GetWarehousesQueryResult>($"/warehouses/{warehouseEntity.Id}");

            // Assert
            result.Should().NotBeNull();
        }

        [Theory]
        [InlineDataFixture]
        public async Task ShouldGetWarehouses(WarehouseEntity warehouseEntity, WarehouseEntity warehouseEntity1)
        {
            // Arrange
            DbContext.Add(warehouseEntity);
            DbContext.SaveChanges();
            DbContext.Add(warehouseEntity1);
            DbContext.SaveChanges();

            // Act
            var result = await HttpClient.GetAsync<IEnumerable<GetWarehousesQueryResult>>($"/warehouses");

            // Assert
            result.Should().HaveCountGreaterThanOrEqualTo(2);
        }
    }
}

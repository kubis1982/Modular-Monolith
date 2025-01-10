namespace ModularMonolith.Modules.ReadModel.Endpoints {
    using ModularMonolith.Modules.ReadModel.Persistance.ReadModel.Warehouses;
    using ModularMonolith.Modules.ReadModel.Queries.Warehouses;
    using ModularMonolith.Shared;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Xunit.Abstractions;

    public class WarehouseTests(WebApplicationFixture webApplicationFixture, ITestOutputHelper testOutputHelper) : ModuleApiTests(webApplicationFixture, testOutputHelper) {

        [Theory]
        [InlineDataFixture]
        public async Task ShouldGetWarehouse(WarehouseEntity warehouseEntity) {
            // Arrange
            DbContext.Add(warehouseEntity);
            DbContext.SaveChanges();

            // Act
            var result = await HttpClient.GetAsync<GetWarehouseQueryResult>($"/wam/warehouses/{warehouseEntity.Id}");

            // Assert
            result.Should().NotBeNull();
        }
    }
}

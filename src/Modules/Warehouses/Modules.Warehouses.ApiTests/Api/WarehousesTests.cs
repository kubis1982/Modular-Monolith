using Castle.Core.Internal;
using Microsoft.Extensions.DependencyInjection;
using ModularMonolith.Modules.Warehouses.Persistance.ReadModel.Entities;
using Xunit.Abstractions;

namespace ModularMonolith.Modules.Warehouses.Api
{
    public class WarehousesTests(WebApplicationFixture webApplicationFixture, ITestOutputHelper testOutputHelper) : ModuleApiTests(webApplicationFixture, testOutputHelper)
    {
        [Theory]
        [InlineDataFixture]
        public async Task ShouldGetWarehouse(WarehouseEntity warehouseEntity, WarehouseEntity warehouseEntity1)
        {
            // Arrange
            DbContext.AddRange(warehouseEntity, warehouseEntity1);
            DbContext.SaveChanges();

            // Act
            var result = await Services.GetRequiredService<IWarehousesModuleApi>().GetWarehouseAsync(warehouseEntity.Id, default);

            // Assert
            result.Code.Should().Be(warehouseEntity.Code);
        }

        [Theory]
        [InlineDataFixture]
        public async Task ShouldGetWarehouses(WarehouseEntity warehouseEntity, WarehouseEntity warehouseEntity1)
        {
            // Arrange
            DbContext.AddRange(warehouseEntity, warehouseEntity1);
            DbContext.SaveChanges();

            // Act
            var result = await Services.GetRequiredService<IWarehousesModuleApi>().GetWarehousesAsync([warehouseEntity.Id, warehouseEntity1.Id], default);

            // Assert
            result.Should().HaveCount(2);
            result.Should().Contain(n => n.Id == warehouseEntity.Id);
            result.Should().Contain(n => n.Id == warehouseEntity1.Id);
        }
    }
}

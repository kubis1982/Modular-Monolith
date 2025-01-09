namespace ModularMonolith.Modules.Warehouses
{
    using Microsoft.EntityFrameworkCore;
    using ModularMonolith.Modules.Warehouses.Domain.Warehouses;
    using ModularMonolith.Modules.Warehouses.Persistance.ReadModel;
    using ModularMonolith.Modules.Warehouses.Persistance.ReadModel.Entities;
    using ModularMonolith.Shared;
    using System.Threading.Tasks;
    using Xunit.Abstractions;

    [Collection(nameof(WebApplicationFixtureCollection))]
    public class ModuleApiTests(WebApplicationFixture webApplicationFixture, ITestOutputHelper testOutputHelper) : ApiTests<ReadDbContext>(webApplicationFixture, testOutputHelper)
    {
        protected WarehouseEntity MainGroup { get; set; } = default!;

        public override async Task InitializeAsync()
        {
            await base.InitializeAsync();

            // Group
            MainGroup = new WarehouseEntity
            {
                TypeId = WarehouseId.Default.TypeId,
                Id = WarehouseId.Default,
                Code = "MAG",
                Name = "Magazyn domyślny"
            };
            DbContext.Warehouses.Add(MainGroup);
            DbContext.SaveChanges();
            DbContext.Database.ExecuteSqlRaw($"""ALTER SEQUENCE "{ModuleDefinition.MODULE_CODE}"."Warehouses_Id_seq" RESTART WITH 2;""");
        }
    }
}

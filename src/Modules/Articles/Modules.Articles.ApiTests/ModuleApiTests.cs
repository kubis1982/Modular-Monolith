namespace ModularMonolith.Modules.Articles
{
    using Microsoft.EntityFrameworkCore;
    using ModularMonolith.Modules.Articles.Domain.MeasurementUnits;
    using ModularMonolith.Modules.Articles.Persistance.ReadModel;
    using ModularMonolith.Modules.Articles.Persistance.ReadModel.Entities;
    using ModularMonolith.Shared;
    using System.Threading.Tasks;
    using Xunit.Abstractions;

    [Collection(nameof(WebApplicationFixtureCollection))]
    public class ModuleApiTests(WebApplicationFixture webApplicationFixture, ITestOutputHelper testOutputHelper) : ApiTests<ReadDbContext>(webApplicationFixture, testOutputHelper)
    {
        public override async Task InitializeAsync()
        {
            await base.InitializeAsync();

            var kgUnit = new MeasurementUnitEntity
            {
                TypeId = MeasurementUnitId.Kg.TypeId,
                Id = MeasurementUnitId.Kg.Id,
                Code = "kg",
                Name = "kilogram"
            };
            DbContext.MeasurementUnits.Add(kgUnit);
            DbContext.SaveChanges();
            DbContext.Database.ExecuteSqlRaw($"""ALTER SEQUENCE "{ModuleDefinition.MODULE_CODE}"."MeasurementUnits_Id_seq" RESTART WITH 2;""");
        }
    }
}

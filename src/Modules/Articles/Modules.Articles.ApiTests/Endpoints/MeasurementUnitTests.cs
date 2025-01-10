using ModularMonolith.Modules.Articles.Persistance.ReadModel.Entities;
using ModularMonolith.Modules.Articles.Requests.Articles;
using Xunit.Abstractions;

namespace ModularMonolith.Modules.Articles.Endpoints
{
    public class MeasurementUnitTests(WebApplicationFixture webApplicationFixture, ITestOutputHelper testOutputHelper) : ModuleApiTests(webApplicationFixture, testOutputHelper)
    {
        [Theory]
        [InlineDataFixture]
        public async Task ShouldCreateMeasurementUnit(CreateMeasurementUnitRequest request)
        {
            // Act
            var identity = await HttpClient.PostAndReturnIdentityAsync($"/measurement-units", request);

            // Assert
            var article = DbContext.MeasurementUnits.Single(n => n.Id == identity.Id);
            article.Name.Should().Be(request.Name);
            article.Code.Should().Be(request.Code);
        }

        [Theory]
        [InlineDataFixture]
        public async Task ShouldUpdateMeasurementUnit(MeasurementUnitEntity unit, UpdateMeasurementUnitRequest request)
        {
            // Arrange
            unit.Code = "code";

            DbContext.Add(unit);
            DbContext.SaveChanges();

            // Act
            await HttpClient.PutAsync($"/measurement-units/{unit.Id}", request);

            // Assert
            unit = DbContext.MeasurementUnits.Single(n => n.Id == unit.Id);
            unit.Name.Should().Be(request.Name);
        }

        [Theory]
        [InlineDataFixture]
        public async Task ShouldDeleteMeasurementUnit(MeasurementUnitEntity unit)
        {
            // Arrange
            unit.Code = "code";

            DbContext.Add(unit);
            DbContext.SaveChanges();

            // Act
            await HttpClient.DeleteAndEnsureNoContentAsync($"/measurement-units/{unit.Id}");

            // Assert
            DbContext.MeasurementUnits.Any(n => n.Id == unit.Id).Should().BeFalse();
        }
    }
}
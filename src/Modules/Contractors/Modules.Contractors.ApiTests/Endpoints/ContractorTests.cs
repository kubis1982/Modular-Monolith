using ModularMonolith.Modules.Contractors.Persistance.ReadModel.Entities;
using ModularMonolith.Modules.Contractors.Requests;
using Xunit.Abstractions;

namespace ModularMonolith.Modules.Contractors.Endpoints
{
    public class ContractorTests(WebApplicationFixture webApplicationFixture, ITestOutputHelper testOutputHelper) : ModuleApiTests(webApplicationFixture, testOutputHelper)
    {
        [Theory]
        [InlineDataFixture]
        public async Task ShouldCreateNewContractor(CreateContractorRequest request)
        {
            // Act
            var identity = await HttpClient.PostAndReturnIdentityAsync($"/contractors", request);

            // Assert
            var result = DbContext.Contractors.Single(n => n.Id == identity.Id);
            result.Name.Should().Be(request.Name);
            result.Code.Should().Be(request.Code);
            result.Description.Should().Be(request.Description);
            result.IsBlocked.Should().BeFalse();
        }

        [Theory]
        [InlineDataFixture]
        public async Task ShouldUpdateContractor(ContractorEntity contractor, UpdateContractorRequest request)
        {
            // Arrange
            DbContext.Add(contractor);
            DbContext.SaveChanges();

            // Act
            await HttpClient.PutAsync($"/contractors/{contractor.Id}", request);

            // Assert
            var result = DbContext.Contractors.Single(n => n.Id == contractor.Id);
            result.Name.Should().Be(request.Name);
            result.Code.Should().Be(request.Code);
            result.Description.Should().Be(request.Description);
            result.IsBlocked.Should().BeFalse();
        }

        [Theory]
        [InlineDataFixture]
        public async Task ShouldDeleteContractor(ContractorEntity contractor)
        {
            // Arrange
            DbContext.Add(contractor);
            DbContext.SaveChanges();

            // Act
            await HttpClient.DeleteAndEnsureNoContentAsync($"/contractors/{contractor.Id}");

            // Assert
            var result = DbContext.Contractors.SingleOrDefault(n => n.Id == contractor.Id);
            result.Should().BeNull();
        }

        [Theory]
        [InlineDataFixture]
        public async Task ShouldBlockContractorAsync(ContractorEntity contractor)
        {
            // Arrange
            contractor.IsBlocked = false;
            DbContext.Add(contractor);
            DbContext.SaveChanges();

            // Act
            await HttpClient.PatchAsync($"/contractors/{contractor.Id}/block");

            // Assert
            var result = DbContext.Contractors.Single(n => n.Id == contractor.Id);
            result.IsBlocked.Should().BeTrue();
        }

        [Theory]
        [InlineDataFixture]
        public async Task ShouldUnblockContractorAsync(ContractorEntity contractor)
        {
            // Arrange
            contractor.IsBlocked = true;
            DbContext.Add(contractor);
            DbContext.SaveChanges();

            // Act
            await HttpClient.PatchAsync($"/contractors/{contractor.Id}/unblock");

            // Assert
            var result = DbContext.Contractors.Single(n => n.Id == contractor.Id);
            result.IsBlocked.Should().BeFalse();
        }
    }
}
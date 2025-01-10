using ModularMonolith.Modules.ReadModel.Persistance.ReadModel.Contractors;
using ModularMonolith.Modules.ReadModel.Queries.Contractors;
using ModularMonolith.Shared;
using Xunit.Abstractions;

namespace ModularMonolith.Modules.ReadModel.Endpoints {

    public class ContractorTests(WebApplicationFixture webApplicationFixture, ITestOutputHelper testOutputHelper) : ModuleApiTests(webApplicationFixture, testOutputHelper) {
        [Theory]
        [InlineDataFixture]
        public async Task ShouldGetContractor(ContractorEntity contractorEntity) {
            // Arrange
            DbContext.Add(contractorEntity);
            DbContext.SaveChanges();

            // Act
            var result = await HttpClient.GetAsync<GetContractorQueryResult>($"/cnm/contractors/{contractorEntity.Id}");

            // Assert
            result.Should().NotBeNull();
            result.Address.Should().BeNull();
        }

        [Theory]
        [InlineDataFixture]
        public async Task ShouldGetContractorWithAddress(ContractorAddressEntity contractorAddressEntity) {
            // Arrange
            contractorAddressEntity.IsDefault = true;

            DbContext.Add(contractorAddressEntity);
            DbContext.SaveChanges();

            // Act
            var result = await HttpClient.GetAsync<GetContractorQueryResult>($"/cnm/contractors/{contractorAddressEntity.Contractor.Id}");

            // Assert
            result.Should().NotBeNull();
            result.Address.Should().NotBeNull();
        }

        [Theory]
        [InlineDataFixture]
        public async Task ShouldGetContractorAddress(ContractorAddressEntity contractorAddressEntity) {
            // Arrange
            contractorAddressEntity.IsDefault = true;
            DbContext.Add(contractorAddressEntity);
            DbContext.SaveChanges();

            // Act
            var result = await HttpClient.GetAsync<GetContractorAddressQueryResult>($"/cnm/contractors/{contractorAddressEntity.Contractor.Id}/addresses/{contractorAddressEntity.Id}");

            // Assert
            result.Should().NotBeNull();
            result.Address.Should().NotBeNull();
            result.Contractor.Should().NotBeNull();
        }
    }
}

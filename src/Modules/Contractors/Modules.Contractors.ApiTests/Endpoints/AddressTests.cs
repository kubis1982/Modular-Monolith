namespace ModularMonolith.Modules.Contractors.Endpoints
{
    using ModularMonolith.Modules.Contractors.Persistance.ReadModel.Entities;
    using ModularMonolith.Modules.Contractors.Queries.Addresses;
    using ModularMonolith.Modules.Contractors.Requests;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Xunit.Abstractions;

    public class AddressTests(WebApplicationFixture webApplicationFixture, ITestOutputHelper testOutputHelper) : ModuleApiTests(webApplicationFixture, testOutputHelper)
    {
        [Theory]
        [InlineDataFixture]
        public async Task ShouldCreateNewAddress(ContractorEntity contractorEntity, CreateAddressRequest request)
        {
            // Arrange
            DbContext.Add(contractorEntity);
            DbContext.SaveChanges();

            // Act
            var addressIdentity = await HttpClient.PostAndReturnIdentityAsync($"/contractors/{contractorEntity.Id}/addresses", request);

            //Assert
            var result = DbContext.Addresses.Single(n => n.Id == addressIdentity.Id);
            result.Line1.Should().Be(request.Line1);
            result.Line2.Should().Be(request.Line2);
            result.PostalCode.Should().Be(request.PostalCode);
            result.City.Should().Be(request.City);
            result.Country.Should().Be(request.Country);
        }

        [Theory]
        [InlineDataFixture]
        public async Task ShouldDeleteAddress(ContractorEntity contractorEntity, AddressEntity addressEntity)
        {
            // Arrange
            DbContext.Add(contractorEntity);
            DbContext.SaveChanges();
            addressEntity.ContractorId = contractorEntity.Id;
            DbContext.Add(addressEntity);
            DbContext.SaveChanges();

            // Act
            await HttpClient.DeleteAndEnsureNoContentAsync($"/contractors/{contractorEntity.Id}/addresses/{addressEntity.Id}");

            // Assert
            var result = DbContext.Addresses.Any(n => n.Id == addressEntity.Id);
            result.Should().Be(false);
        }

        [Theory]
        [InlineDataFixture]
        public async Task ShouldUpdateAddress(ContractorEntity contractorEntity, AddressEntity addressEntity, UpdateAddressRequest request)
        {
            // Arrange
            DbContext.Add(contractorEntity);
            DbContext.SaveChanges();
            addressEntity.ContractorId = contractorEntity.Id;
            DbContext.Add(addressEntity);
            DbContext.SaveChanges();

            // Act
            await HttpClient.PutAsync($"contractors/{contractorEntity.Id}/addresses/{addressEntity.Id}", request);

            // Assert
            var result = DbContext.Addresses.Single(n => n.Id == addressEntity.Id);
            result.Line1.Should().Be(request.Line1);
            result.Line2.Should().Be(request.Line2);
            result.PostalCode.Should().Be(request.PostalCode);
            result.City.Should().Be(request.City);
            result.Country.Should().Be(request.Country);
        }

        [Theory]
        [InlineDataFixture]
        public async Task ShouldSetDefaultAddress(ContractorEntity contractorEntity, AddressEntity addressEntity1, AddressEntity addressEntity2)
        {
            // Arrange
            DbContext.Add(contractorEntity);
            DbContext.SaveChanges();
            addressEntity1.ContractorId = contractorEntity.Id;
            addressEntity1.IsDefault = true;
            DbContext.Add(addressEntity1);
            DbContext.SaveChanges();
            addressEntity2.ContractorId = contractorEntity.Id;
            addressEntity2.IsDefault = false;
            DbContext.Add(addressEntity2);
            DbContext.SaveChanges();

            // Act
            await HttpClient.PatchAsync($"contractors/{contractorEntity.Id}/addresses/{addressEntity2.Id}/default");

            // Assert
            var result1 = DbContext.Addresses.Single(n => n.Id == addressEntity1.Id);
            var result2 = DbContext.Addresses.Single(n => n.Id == addressEntity2.Id);

            result1.IsDefault.Should().BeFalse();
            result2.IsDefault.Should().BeTrue();
        }

        [Theory]
        [InlineDataFixture]
        public async Task ShouldGetAddressesAsync(ContractorEntity contractorEntity, AddressEntity addressEntity)
        {
            // Arrange
            DbContext.Add(contractorEntity);
            DbContext.SaveChanges();
            addressEntity.ContractorId = contractorEntity.Id;
            DbContext.Add(addressEntity);
            DbContext.SaveChanges();

            // Act
            var result = await HttpClient.GetAsync<IEnumerable<GetAddressesQueryResult>>($"/contractors/{contractorEntity.Id}/addresses");

            // Assert            
            result.Should().NotBeNull();
        }

        [Theory]
        [InlineDataFixture]
        public async Task ShouldGetAddressAsync(ContractorEntity contractorEntity, AddressEntity addressEntity1, AddressEntity addressEntity2)
        {
            // Arrange
            DbContext.Add(contractorEntity);
            DbContext.SaveChanges();
            addressEntity1.ContractorId = contractorEntity.Id;
            DbContext.Add(addressEntity1);
            DbContext.SaveChanges();
            addressEntity2.ContractorId = contractorEntity.Id;
            DbContext.Add(addressEntity2);
            DbContext.SaveChanges();

            // Act
            var result = await HttpClient.GetAsync<GetAddressesQueryResult>($"/contractors/{contractorEntity.Id}/addresses/{addressEntity2.Id}");

            // Assert            
            result.Id.Should().Be(addressEntity2.Id);
            result.Line1.Should().Be(addressEntity2.Line1);
        }
    }
}

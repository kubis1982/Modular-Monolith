namespace ModularMonolith.Modules.Contractors.Domain.Contractors
{
    public class ContractorTests : ModuleDomainTests
    {
        [Theory]
        [InlineDataFixture]
        public void ShouldCreateContractor(string name, string code, string description, int contractorId, Country country, Address address)
        {
            Contractor contractor = Contractor.Create(code, name, description, country, address).Extensions().SetValue(n => n.Id, new ContractorId(contractorId)).DomainEntity;

            var @event = contractor.Extensions().GetEvent<Events.ContractorCreatedEvent>();
            @event.ContractorId.Should().Be(new ContractorId(contractorId));
            @event.Code.Should().Be(ContractorCode.Of(code));
            @event.Name.Should().Be(ContractorName.Of(name));
            @event.Description.Should().Be(description);
        }

        [Theory]
        [InlineDataFixture]
        public void ShouldUpdateContractor(Contractor contractor, string newCode, string newName, string newDescription, Country country, Address address)
        {
            contractor.Update(newCode, newName, newDescription, country, 0, address);
            var @event = contractor.Extensions().GetEvent<Events.ContractorUpdatedEvent>();
            @event.ContractorId.Should().Be(contractor.Id);
            @event.Code.Should().Be(ContractorCode.Of(newCode));
            @event.Name.Should().Be(ContractorName.Of(newName));
            @event.Description.Should().Be(newDescription);
        }

        [Theory]
        [InlineDataFixture]
        public void ShouldRemoveContractor(Contractor contractor)
        {
            // Arrange
            Mock<IContractorUsageService> contractorUsageService = new Mock<IContractorUsageService>();

            // Act
            contractor.Remove(contractorUsageService.Object);

            // Assert
            var @event = contractor.Extensions().GetEvent<Events.ContractorRemovedEvent>();
            @event.ContractorId.Should().Be(contractor.Id);
        }

        [Theory]
        [InlineDataFixture]
        public void ShouldBlockContractor(Contractor contractor)
        {
            contractor.Block();
            var @event = contractor.Extensions().GetEvent<Events.ContractorBlockedEvent>();
            @event.ContractorId.Should().Be(contractor.Id);
        }

        [Theory]
        [InlineDataFixture]
        public void ShouldUnblockContractor(Contractor contractor)
        {
            // Arrange
            contractor.Extensions().SetValue(n => n.IsBlocked, true);

            // Act
            contractor.Unblock();

            // Assert
            var @event = contractor.Extensions().GetEvent<Events.ContractorUnblockedEvent>();
            @event.ContractorId.Should().Be(contractor.Id);
        }

        [Theory]
        [InlineDataFixture]
        public void ShouldCreateAddress(Contractor contractor, int addressId, Address address)
        {
            contractor.CreateAddress(address).Extensions().SetValue(n => n.Id, addressId);

            var @event = contractor.Extensions().GetEvent<Events.ContractorCreatedAddressEvent>();

            @event.Address.Line1.Should().Be(address.Line1);
            @event.Address.Line2.Should().Be(address.Line2);
            @event.Address.PostalCode.Should().Be(address.PostalCode);
            @event.Address.City.Should().Be(address.City);
            @event.Address.Country.Should().Be(address.Country);
        }

        [Theory]
        [InlineDataFixture]
        public void ShouldUpdateAddress(Contractor contractor, int addressId, Address address, Address newAddress)
        {

            var contractorAddress = ContractorAddress.Create(address).Extensions().SetValue(n => n.Id, addressId).DomainEntity;

            contractor.Extensions().SetValue(n => n.Addresses, [contractorAddress]);

            contractor.UpdateAddress(contractorAddress.Id, newAddress);

            var @event = contractor.Extensions().GetEvent<Events.ContractorAddressUpdatedEvent>();
            @event.Address.Line1.Should().Be(newAddress.Line1);
            @event.Address.Line2.Should().Be(newAddress.Line2);
            @event.Address.PostalCode.Should().Be(newAddress.PostalCode);
            @event.Address.City.Should().Be(newAddress.City);
            @event.Address.Country.Should().Be(newAddress.Country);
        }
    }
}

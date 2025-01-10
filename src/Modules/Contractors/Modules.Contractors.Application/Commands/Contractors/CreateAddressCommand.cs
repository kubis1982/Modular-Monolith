namespace ModularMonolith.Modules.Contractors.Commands.Contractors
{
    using ModularMonolith.Modules.Contractors.Domain.Contractors;
    using System.Threading;
    using System.Threading.Tasks;

    public sealed record CreateAddressCommand(int ContractorId) : EntityCommand
    {
        public string? AddressLine1 { get; set; }
        public string? AddressLine2 { get; set; }
        public string? AddressPostalCode { get; set; }
        public required string? AddressCity { get; set; }
        public required string? AddressCountry { get; set; }
        internal class CreateAddressCommandHandler(IContractorRepository contractorRepository) : EntityCommandHandler<CreateAddressCommand>
        {
            public override async Task<EntityIdentityResult> Handle(CreateAddressCommand command, CancellationToken cancellationToken)
            {
                Contractor contractor = await contractorRepository.SingleAsync(ContractorSpec.ByIdWithAddresses(command.ContractorId), cancellationToken);
                Country country = Country.Of(command.AddressCountry);
                City city = City.Of(command.AddressCity);
                Address address = Address.Create(command.AddressLine1, command.AddressLine2, command.AddressPostalCode, city, country);
                var contractorAddress = contractor.CreateAddress(address);
                return EntityIdentityResult.Create(contractorAddress);
            }
        }
    }
}

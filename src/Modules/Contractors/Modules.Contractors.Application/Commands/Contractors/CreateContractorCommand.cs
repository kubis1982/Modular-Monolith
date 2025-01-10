namespace ModularMonolith.Modules.Contractors.Commands.Contractors
{
    using System.Threading;
    using System.Threading.Tasks;

    public record CreateContractorCommand : EntityCommand
    {
        public required string Code { get; init; }
        public required string Name { get; init; }
        public required string Country { get; init; }
        public string? Description { get; init; }
        public string? AddressLine1 { get; init; }
        public string? AddressLine2 { get; init; }
        public string? AddressPostalCode { get; init; }
        public required string? AddressCity { get; init; }
        public required string? AddressCountry { get; init; }

        internal class CreateContractorCommandHandler(IContractorRepository contractorRepository) : EntityCommandHandler<CreateContractorCommand>
        {
            public override async Task<EntityIdentityResult> Handle(CreateContractorCommand command, CancellationToken cancellationToken)
            {
                ContractorName name = ContractorName.Of(command.Name);
                ContractorCode code = ContractorCode.Of(command.Code);
                Country country = Domain.Contractors.Country.Of(command.Country);
                Country addressCountry = Domain.Contractors.Country.Of(command.AddressCountry ?? command.Country);
                City city = City.Of(command.AddressCity);
                Address address = Address.Create(command.AddressLine1, command.AddressLine2, command.AddressPostalCode, city, addressCountry);
                Contractor contractor = Contractor.Create(code, name, command.Description, country, address);
                await contractorRepository.AddAsync(contractor, cancellationToken);
                return EntityIdentityResult.Create(contractor);
            }
        }
    }
}

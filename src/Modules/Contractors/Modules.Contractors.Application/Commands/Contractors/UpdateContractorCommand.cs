namespace ModularMonolith.Modules.Contractors.Commands.Contractors
{
    using System.Threading;
    using System.Threading.Tasks;

    public sealed record UpdateContractorCommand(int ContractorId) : UnitOfWorkCommand
    {
        public required int? AddressId { get; init; }
        public required string Code { get; init; }
        public required string Name { get; init; }
        public required string? Country { get; init; }
        public required string? Description { get; init; }
        public required string? AddressLine1 { get; init; }
        public required string? AddressLine2 { get; init; }
        public required string? AddressPostalCode { get; init; }
        public required string? AddressCity { get; init; }
        public required string? AddressCountry { get; init; }
        internal class UpdateContractorCommandHandler(IContractorRepository contractorRepository) : UnitOfWorkCommandHandler<UpdateContractorCommand>
        {
            public override async Task Handle(UpdateContractorCommand command, CancellationToken cancellationToken)
            {
                Contractor contractor = await contractorRepository.SingleAsync(ContractorSpec.ByIdWithAddress(command.ContractorId, command.AddressId ?? 0), cancellationToken);
                Country country = Domain.Contractors.Country.Of(command.Country);
                Country addressCountry = Domain.Contractors.Country.Of(command.AddressCountry ?? command.Country);
                City city = City.Of(command.AddressCity);
                Address address = Address.Create(command.AddressLine1, command.AddressLine2, command.AddressPostalCode, city, addressCountry);
                contractor.Update(command.Code, command.Name, command.Description, country, command.AddressId, address);
            }
        }
    }
}

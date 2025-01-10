namespace ModularMonolith.Modules.Contractors.Commands.Contractors
{
    using ModularMonolith.Modules.Contractors.Domain.Contractors;
    using System.Threading;
    using System.Threading.Tasks;

    public sealed record UpdateAddressCommand(int ContractorId, int AddressId) : UnitOfWorkCommand
    {
        public required string? AddressLine1 { get; set; }
        public required string? AddressLine2 { get; set; }
        public required string? AddressPostalCode { get; set; }
        public required string? AddressCity { get; set; }
        public required string? AddressCountry { get; set; }
        internal class UpdateAddressCommandHandler(IContractorRepository contractorRepository) : UnitOfWorkCommandHandler<UpdateAddressCommand>
        {
            public override async Task Handle(UpdateAddressCommand command, CancellationToken cancellationToken)
            {
                Contractor contractor = await contractorRepository.SingleAsync(ContractorSpec.ByIdWithAddress(command.ContractorId, command.AddressId), cancellationToken);
                Country country = Country.Of(command.AddressCountry);
                City city = City.Of(command.AddressCity);
                Address address = Address.Create(command.AddressLine1, command.AddressLine2, command.AddressPostalCode, city, country);
                contractor.UpdateAddress(command.AddressId, address);
            }
        }
    }
}

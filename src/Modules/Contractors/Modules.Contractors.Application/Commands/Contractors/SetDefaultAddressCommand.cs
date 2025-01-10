namespace ModularMonolith.Modules.Contractors.Commands.Contractors
{
    using ModularMonolith.Modules.Contractors.Domain.Contractors;
    using System.Threading;
    using System.Threading.Tasks;

    public sealed record SetDefaultAddressCommand(int ContractorId, int AddressId) : UnitOfWorkCommand
    {
        internal class SetDefaultAddressCommandHandler(IContractorRepository contractorRepository) : UnitOfWorkCommandHandler<SetDefaultAddressCommand>
        {
            public override async Task Handle(SetDefaultAddressCommand command, CancellationToken cancellationToken)
            {
                Contractor contractor = await contractorRepository.SingleAsync(ContractorSpec.ByIdWithAddresses(command.ContractorId), cancellationToken);
                contractor.SetDefaultAddress(command.AddressId);
            }
        }
    }
}

namespace ModularMonolith.Modules.Contractors.Commands.Contractors
{
    using System.Threading;
    using System.Threading.Tasks;

    public sealed record DeleteAddressCommand(int ContractorId, int AddressId) : UnitOfWorkCommand
    {
        internal class DeleteAddressCommandHandler(IContractorRepository contractorRepository) : UnitOfWorkCommandHandler<DeleteAddressCommand>
        {
            public override async Task Handle(DeleteAddressCommand command, CancellationToken cancellationToken)
            {
                Contractor contractor = await contractorRepository.SingleAsync(ContractorSpec.ByIdWithAddress(command.ContractorId, command.AddressId), cancellationToken);
                contractor.RemoveAddress(command.AddressId);
            }
        }
    }
}

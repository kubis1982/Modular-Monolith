namespace ModularMonolith.Modules.Contractors.Commands.Contractors
{
    using System.Threading;
    using System.Threading.Tasks;

    public sealed record BlockContractorCommand(int ContractorId) : UnitOfWorkCommand
    {
        internal class BlockContractorCommandHandler(IContractorRepository contractorRepository) : UnitOfWorkCommandHandler<BlockContractorCommand>
        {
            public override async Task Handle(BlockContractorCommand command, CancellationToken cancellationToken)
            {
                Contractor contractor = await contractorRepository.SingleAsync(ContractorSpec.ById(command.ContractorId), cancellationToken);
                contractor.Block();
            }
        }
    }
}

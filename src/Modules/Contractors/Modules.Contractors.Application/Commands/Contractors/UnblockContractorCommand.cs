namespace ModularMonolith.Modules.Contractors.Commands.Contractors
{
    using System.Threading;
    using System.Threading.Tasks;

    public sealed record UnblockContractorCommand(int ContractorId) : UnitOfWorkCommand
    {
        internal class UnblockContractorCommandHandler(IContractorRepository contractorRepository) : UnitOfWorkCommandHandler<UnblockContractorCommand>
        {
            public override async Task Handle(UnblockContractorCommand command, CancellationToken cancellationToken)
            {
                Contractor contractor = await contractorRepository.SingleAsync(ContractorSpec.ById(command.ContractorId), cancellationToken);
                contractor.Unblock();
            }
        }
    }
}

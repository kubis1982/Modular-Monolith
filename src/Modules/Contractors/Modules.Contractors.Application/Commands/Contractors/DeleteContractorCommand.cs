namespace ModularMonolith.Modules.Contractors.Commands.Contractors
{
    using System.Threading;
    using System.Threading.Tasks;

    public sealed record DeleteContractorCommand(int ContractorId) : UnitOfWorkCommand
    {
        internal class DeleteContractorCommandHandler(IContractorRepository contractorRepository, IContractorUsageService contractorUsageService) : UnitOfWorkCommandHandler<DeleteContractorCommand>
        {
            public override async Task<EntityIdentityResult> Handle(DeleteContractorCommand command, CancellationToken cancellationToken)
            {
                var contractor = await contractorRepository.SingleAsync(ContractorSpec.ById(command.ContractorId), cancellationToken);
                contractor.Remove(contractorUsageService);
                await contractorRepository.DeleteAsync(contractor, cancellationToken);
                return EntityIdentityResult.Create(contractor);
            }
        }
    }
}

namespace ModularMonolith.Modules.Contractors.Api
{
    using ModularMonolith.Modules.Contractors.Api.Dtos;
    using ModularMonolith.Modules.Contractors.Api.Queries;
    using ModularMonolith.Modules.Contractors.Domain.Contractors.Exceptions;
    using ModularMonolith.Shared.CQRS.Queries;
    using System.Threading;
    using System.Threading.Tasks;

    internal class ContractorsModuleApi(IQueryExecutor queryExecutor) : IContractorsModuleApi
    {
        public async Task<ContractorDto> GetContractorAsync(int contractorId, CancellationToken cancellationToken)
        {
            var contractor = await queryExecutor.Execute(new GetContractorQuery(contractorId), cancellationToken);
            return contractor ?? throw new ContractorNotFoundException(contractorId);
        }
    }
}

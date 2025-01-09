namespace ModularMonolith.Modules.Ordering.Services
{
    using AutoMapper;
    using ModularMonolith.Modules.Contractors.Api;
    using System.Threading;
    using System.Threading.Tasks;

    internal class ContractorsService(IContractorsModuleApi contractorsModuleApi, IMapper mapper) : IContractorsService
    {
        public async Task<Contractor> GetContractorAsync(int contractorId, CancellationToken cancellationToken)
        {
            return mapper.Map<Contractor>(await contractorsModuleApi.GetContractorAsync(contractorId, cancellationToken));
        }
    }
}

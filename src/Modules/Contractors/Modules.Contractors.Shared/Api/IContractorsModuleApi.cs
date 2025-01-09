namespace ModularMonolith.Modules.Contractors.Api
{
    using ModularMonolith.Modules.Contractors.Api.Dtos;

    public interface IContractorsModuleApi
    {
        Task<ContractorDto> GetContractorAsync(int contractorId, CancellationToken cancellationToken);
    }
}

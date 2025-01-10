namespace ModularMonolith.Modules.Ordering.Services
{
    using ModularMonolith.Modules.Ordering.Domain;

    public interface IContractorsService
    {
        public Task<Contractor> GetContractorAsync(int contractorId, CancellationToken cancellationToken);
    }
}

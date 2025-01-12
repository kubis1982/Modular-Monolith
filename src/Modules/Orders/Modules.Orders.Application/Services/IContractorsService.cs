namespace ModularMonolith.Modules.Orders.Services
{
    using ModularMonolith.Modules.Orders.Domain;

    public interface IContractorsService
    {
        public Task<Contractor> GetContractorAsync(int contractorId, CancellationToken cancellationToken);
    }
}

namespace ModularMonolith.Modules.Contractors.Persistance.WriteModel.Repositories
{
    using ModularMonolith.Modules.Contractors.Domain.Contractors;

    internal class ContractorRepository : Repository<Contractor, ContractorSpec>, IContractorRepository
    {
        public ContractorRepository(WriteDbContext dbContext) : base(dbContext)
        {
        }
    }
}

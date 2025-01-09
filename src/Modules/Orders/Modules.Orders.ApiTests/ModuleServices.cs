namespace ModularMonolith.Modules.Ordering
{
    using ModularMonolith.Modules.Ordering.Domain;
    using ModularMonolith.Modules.Ordering.Services;
    using System.Threading.Tasks;

    public class ModuleServices : IContractorsService, IWarehousesService, IArticlesService
    {
        public Task<Contractor> GetContractorAsync(int contractorId, CancellationToken cancellationToken)
        {
            return Task.FromResult(new Contractor(new ContractorId("CnM01", contractorId), $"Code_{contractorId}", $"Name_{contractorId}", false));
        }

        public Task<Article> GetArticleAsync(int articleId, CancellationToken cancellationToken)
        {
            return Task.FromResult(new Article(new ArticleId("ArM01", articleId), $"Code_{articleId}", $"Name_{articleId}", "kg", false));
        }

        public Task<Warehouse> GetWarehouseAsync(int warehouseId, CancellationToken cancellationToken)
        {
            return Task.FromResult(new Warehouse(new WarehouseId("WaM01", warehouseId), $"Code_{warehouseId}", $"Name_{warehouseId}", false));
        }
    }
}

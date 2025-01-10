namespace ModularMonolith.Modules.ReadModel.Controllers
{
    using ModularMonolith.Modules.ReadModel;
    using ModularMonolith.Modules.ReadModel.Queries.Articles.Queryables;
    using ModularMonolith.Shared.CQRS.Queries;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    [ControllerRoute(Modules.Articles)]
    public class ArticlesController(IQueryExecutor queryExecutor) : ModuleController {
        [MethodRoute("articles")]
        [Documentation(Modules.Articles, "GetArticles", "OOOOO")]
        public Task<IQueryable<GetArticlesQueryResult>> GetArticles(CancellationToken cancellationToken) => queryExecutor.Execute(new GetArticlesQuery(), cancellationToken);
    }
}

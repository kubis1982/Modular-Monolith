namespace ModularMonolith.Modules.Articles.Persistance.WriteModel.Repositories
{
    internal class ArticleRepository : Repository<Article, ArticleSpec>, IArticleRepository
    {

        public ArticleRepository(WriteDbContext dbContext) : base(dbContext)
        {
        }
    }
}
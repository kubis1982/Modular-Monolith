namespace ModularMonolith.Modules.Articles.Domain.Articles
{
    using ModularMonolith.Shared.Kernel.Types;
    using System;
    using System.Linq;

    public class ArticleSpec : Specification<Article>, ISingleResultSpecification<Article>
    {
        private ArticleSpec(Action<ISpecificationBuilder<Article>> action)
            => action(Query);

        public static ArticleSpec ById(ArticleId articleId)
            => new(query => query.Where(n => n.Id == articleId));
    }
}

namespace ModularMonolith.Modules.Articles.Domain.Articles
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public interface IArticleRepository
    {
        /// <summary>
        /// Adds a <see cref="Article"/>.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Article> AddAsync(Article entity, CancellationToken cancellationToken);

        /// <summary>
        /// Gets a <see cref="Article"/>.
        /// </summary>
        /// <param name="spec"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Article> SingleAsync(ArticleSpec spec, CancellationToken cancellationToken);

        /// <summary>
        /// Removes a <see cref="Article"/>.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task DeleteAsync(Article entity, CancellationToken cancellationToken);

        /// <summary>
        /// Gets a list of Articles
        /// </summary>
        /// <param name="spec"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<List<Article>> ListAsync(ISpecification<Article> spec, CancellationToken cancellationToken);
    }
}

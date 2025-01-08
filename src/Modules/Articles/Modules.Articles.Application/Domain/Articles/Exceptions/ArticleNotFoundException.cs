namespace ModularMonolith.Modules.Articles.Domain.Articles.Exceptions
{
    using ModularMonolith.Shared.Exceptions;

    public class ArticleNotFoundException : EntityNotFoundException
    {
        public ArticleNotFoundException(int articleId) : base($"Artykuł: {articleId}")
        {
        }
    }
}

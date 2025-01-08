namespace ModularMonolith.Modules.Articles.Commands.Articles
{
    using System.Threading;
    using System.Threading.Tasks;

    public sealed record DeleteArticleCommand(int ArticleId) : UnitOfWorkCommand
    {
        internal class DeleteArticleCommandHandler(IArticleRepository articleRepository, IArticleUsageService articleUsageService) : UnitOfWorkCommandHandler<DeleteArticleCommand>
        {
            public override async Task Handle(DeleteArticleCommand request, CancellationToken cancellationToken)
            {
                var article = await articleRepository.SingleAsync(ArticleSpec.ById(request.ArticleId), cancellationToken);
                article.Remove(articleUsageService);
                await articleRepository.DeleteAsync(article, cancellationToken);
            }
        }
    }
}

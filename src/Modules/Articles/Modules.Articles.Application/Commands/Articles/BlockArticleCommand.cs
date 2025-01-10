namespace ModularMonolith.Modules.Articles.Commands.Articles
{

    using System.Threading;
    using System.Threading.Tasks;

    public sealed record BlockArticleCommand(int ArticleId) : UnitOfWorkCommand
    {
        internal class BlockArticleCommandHandler(IArticleRepository articleRepository) : UnitOfWorkCommandHandler<BlockArticleCommand>
        {
            public override async Task Handle(BlockArticleCommand request, CancellationToken cancellationToken)
            {
                Article article = await articleRepository.SingleAsync(ArticleSpec.ById(request.ArticleId), cancellationToken);
                article.Block();
            }
        }
    }
}

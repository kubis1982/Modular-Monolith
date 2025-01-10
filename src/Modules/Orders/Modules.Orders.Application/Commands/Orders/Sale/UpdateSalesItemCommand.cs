namespace ModularMonolith.Modules.Ordering.Commands.Orders.Sale
{
    using FluentValidation;
    using ModularMonolith.Modules.Ordering.Domain;
    using ModularMonolith.Modules.Ordering.Domain.Orders;
    using ModularMonolith.Modules.Ordering.Services;
    using System.Threading;
    using System.Threading.Tasks;

    public sealed record UpdateSalesItemCommand(int OrderId, int ItemId) : UnitOfWorkCommand
    {
        public required int ArticleId { get; set; }
        public required decimal Quantity { get; set; }
        public required string Unit { get; set; }
        public required int Numerator { get; init; } = 1;
        public required int Denominator { get; init; } = 1;
        public required string? Description { get; set; }
        internal class UpdateSalesItemCommandHandler(IOrderRepository orderRepository, IArticlesService articlesService) : UnitOfWorkCommandHandler<UpdateSalesItemCommand>
        {
            public override async Task Handle(UpdateSalesItemCommand command, CancellationToken cancellationToken)
            {
                Order order = await orderRepository.SingleAsync(OrderSpec.ByIdWithItem(OrderType.Sale, command.OrderId, command.ItemId), cancellationToken);
                Article article = await articlesService.GetArticleAsync(command.ArticleId, cancellationToken);
                order.UpdateItem(command.ItemId, article, Domain.Quantity.Create(command.Quantity, command.Unit, command.Numerator, command.Denominator), command.Description);
            }
        }

        internal class UpdateSalesItemCommandValidator : AbstractValidator<UpdateSalesItemCommand>
        {
            public UpdateSalesItemCommandValidator()
            {
                RuleFor(n => n.Unit)
                    .NotEmpty();
                RuleFor(n => n.Numerator)
                    .LessThanOrEqualTo(1);
                RuleFor(n => n.Denominator)
                    .LessThanOrEqualTo(1);
            }
        }
    }
}

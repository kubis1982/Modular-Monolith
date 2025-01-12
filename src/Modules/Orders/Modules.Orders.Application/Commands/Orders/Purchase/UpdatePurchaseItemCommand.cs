namespace ModularMonolith.Modules.Orders.Commands.Orders.Purchase
{
    using FluentValidation;
    using ModularMonolith.Modules.Orders.Domain;
    using ModularMonolith.Modules.Orders.Domain.Orders;
    using ModularMonolith.Modules.Orders.Services;
    using System.Threading;
    using System.Threading.Tasks;

    public sealed record UpdatePurchaseItemCommand(int OrderId, int ItemId) : UnitOfWorkCommand
    {
        public required int ArticleId { get; set; }
        public required decimal Quantity { get; set; }
        public required string Unit { get; set; }
        public required int Numerator { get; init; } = 1;
        public required int Denominator { get; init; } = 1;
        public required string? Description { get; set; }
        internal class UpdatePurchaseItemCommandHandler(IOrderRepository orderRepository, IArticlesService articlesService) : UnitOfWorkCommandHandler<UpdatePurchaseItemCommand>
        {
            public override async Task Handle(UpdatePurchaseItemCommand command, CancellationToken cancellationToken)
            {
                Order order = await orderRepository.SingleAsync(OrderSpec.ByIdWithItem(OrderType.Purchase, command.OrderId, command.ItemId), cancellationToken);
                Article article = await articlesService.GetArticleAsync(command.ArticleId, cancellationToken);
                order.UpdateItem(command.ItemId, article, Domain.Quantity.Create(command.Quantity, command.Unit, command.Numerator, command.Denominator), command.Description);
            }
        }

        internal class UpdatePurchaseItemCommandValidator : AbstractValidator<UpdatePurchaseItemCommand>
        {
            public UpdatePurchaseItemCommandValidator()
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

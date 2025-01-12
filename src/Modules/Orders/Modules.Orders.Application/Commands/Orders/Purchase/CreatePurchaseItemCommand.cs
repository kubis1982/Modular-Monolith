namespace ModularMonolith.Modules.Orders.Commands.Orders.Purchase
{
    using FluentValidation;
    using ModularMonolith.Modules.Orders.Domain;
    using ModularMonolith.Modules.Orders.Domain.Orders;
    using ModularMonolith.Modules.Orders.Extensions;
    using ModularMonolith.Modules.Orders.Services;
    using System.Threading;
    using System.Threading.Tasks;

    public sealed record CreatePurchaseItemCommand(int OrderId) : EntityCommand
    {
        public required int ArticleId { get; set; }
        public required decimal Quantity { get; set; }
        public required string Unit { get; set; }
        public int Numerator { get; set; } = 1;
        public int Denominator { get; set; } = 1;
        public string? Description { get; set; }
        internal class CreatePurchaseItemCommandHandler(IOrderRepository orderRepository, IArticlesService articlesService) : EntityCommandHandler<CreatePurchaseItemCommand>
        {
            public override async Task<EntityIdentityResult> Handle(CreatePurchaseItemCommand command, CancellationToken cancellationToken)
            {
                Order order = await orderRepository.SingleAsync(OrderSpec.ByIdWithLastPosition(OrderType.Purchase, command.OrderId), cancellationToken);
                Article article = await articlesService.GetArticleAsync(command.ArticleId, cancellationToken);
                OrderItem item = order.AddItem(article, Domain.Quantity.Create(command.Quantity, command.Unit, command.Numerator, command.Denominator), command.Description);
                return item.CreateEntityIdentityResult();
            }
        }

        internal class CreatePurchaseItemCommandValidator : AbstractValidator<CreatePurchaseItemCommand>
        {
            public CreatePurchaseItemCommandValidator()
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

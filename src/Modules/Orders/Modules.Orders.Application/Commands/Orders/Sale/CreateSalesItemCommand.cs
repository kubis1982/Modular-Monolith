namespace ModularMonolith.Modules.Ordering.Commands.Orders.Sale
{
    using FluentValidation;
    using ModularMonolith.Modules.Ordering.Domain;
    using ModularMonolith.Modules.Ordering.Domain.Orders;
    using ModularMonolith.Modules.Ordering.Extensions;
    using ModularMonolith.Modules.Ordering.Services;
    using System.Threading;
    using System.Threading.Tasks;

    public sealed record CreateSalesItemCommand(int OrderId) : EntityCommand
    {
        public required int ArticleId { get; set; }
        public required decimal Quantity { get; set; }
        public required string Unit { get; set; }
        public int Numerator { get; init; } = 1;
        public int Denominator { get; init; } = 1;
        public string? Description { get; set; }
        internal class CreateSalesItemCommandHandler(IOrderRepository orderRepository, IArticlesService articlesService) : EntityCommandHandler<CreateSalesItemCommand>
        {
            public override async Task<EntityIdentityResult> Handle(CreateSalesItemCommand command, CancellationToken cancellationToken)
            {
                Order order = await orderRepository.SingleAsync(OrderSpec.ByIdWithLastPosition(OrderType.Sale, command.OrderId), cancellationToken);
                Article article = await articlesService.GetArticleAsync(command.ArticleId, cancellationToken);
                OrderItem item = order.AddItem(article, Domain.Quantity.Create(command.Quantity, command.Unit, command.Numerator, command.Denominator), command.Description);
                return item.CreateEntityIdentityResult();
            }
        }

        internal class CreateSalesItemCommandValidator : AbstractValidator<CreateSalesItemCommand>
        {
            public CreateSalesItemCommandValidator()
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

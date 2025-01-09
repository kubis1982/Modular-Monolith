namespace ModularMonolith.Modules.Ordering.Domain.Orders.Events
{
    public sealed record OrderItemUpdatedEvent : OrderDomainEvent
    {
        public OrderItemUpdatedEvent(Order order, OrderItemId orderItemId, ArticleId articleId, Quantity quantity, string? description) : base(order)
        {
            OrderItemId = orderItemId;
            ArticleId = articleId;
            Quantity = quantity;
            Description = description;
        }

        public OrderItemId OrderItemId { get; }
        public ArticleId ArticleId { get; }
        public Quantity Quantity { get; }
        public string? Description { get; }
    }
}
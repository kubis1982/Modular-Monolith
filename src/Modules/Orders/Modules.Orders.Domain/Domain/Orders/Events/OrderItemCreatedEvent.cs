namespace ModularMonolith.Modules.Ordering.Domain.Orders.Events
{
    public sealed record OrderItemCreatedEvent : OrderDomainEvent
    {
        private readonly OrderItem orderItem;

        public OrderItemCreatedEvent(Order order, OrderItem orderItem, ArticleId articleId, Quantity quantity, string? description) : base(order)
        {
            this.orderItem = orderItem;
            ArticleId = articleId;
            Quantity = quantity;
            Description = description;
        }

        public OrderItemId OrderItemId => orderItem.Id;
        public ArticleId ArticleId { get; }
        public Quantity Quantity { get; }
        public string? Description { get; }
    }
}
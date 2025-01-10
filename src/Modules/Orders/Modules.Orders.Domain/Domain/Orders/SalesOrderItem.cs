namespace ModularMonolith.Modules.Ordering.Domain.Orders
{
    /// <summary>
    /// Represents a sales order item.
    /// </summary>
    public sealed class SalesOrderItem : OrderItem
    {
        private SalesOrderItem()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SalesOrderItem"/> class with the specified order, article, and quantity.
        /// </summary>
        /// <param name="order">The sales order.</param>
        /// <param name="article">The article.</param>
        /// <param name="quantity">The quantity.</param>
        /// <param name="description"></param>
        private SalesOrderItem(Order order, Article article, Quantity quantity, string? description) : base(order, article, quantity, description)
        {
        }

        /// <summary>
        /// Creates a new instance of the <see cref="SalesOrderItem"/> class with the specified order, article, and quantity.
        /// </summary>
        /// <param name="order">The sales order.</param>
        /// <param name="article">The article.</param>
        /// <param name="quantity">The quantity.</param>
        /// <param name="description"></param>
        /// <returns>A new instance of the <see cref="SalesOrderItem"/> class.</returns>
        public static SalesOrderItem Create(SalesOrder order, Article article, Quantity quantity, string? description) => new(order, article, quantity, description);
    }
}

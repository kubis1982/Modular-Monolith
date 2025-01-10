namespace ModularMonolith.Modules.Ordering.Domain.Orders
{
    /// <summary>
    /// Represents a purchase order item.
    /// </summary>
    public sealed class PurchaseOrderItem : OrderItem
    {
        private PurchaseOrderItem()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PurchaseOrderItem"/> class with the specified parent order, article, and quantity.
        /// </summary>
        /// <param name="parent">The parent purchase order.</param>
        /// <param name="article">The article associated with the item.</param>
        /// <param name="quantity">The quantity of the item.</param>
        /// <param name="description"></param>
        private PurchaseOrderItem(Order parent, Article article, Quantity quantity, string? description) : base(parent, article, quantity, description)
        {
        }

        /// <summary>
        /// Creates a new instance of the <see cref="PurchaseOrderItem"/> class with the specified parent order, article, and quantity.
        /// </summary>
        /// <param name="parent">The parent purchase order.</param>
        /// <param name="article">The article associated with the item.</param>
        /// <param name="quantity">The quantity of the item.</param>
        /// <param name="description"></param>
        /// <returns>A new instance of the <see cref="PurchaseOrderItem"/> class.</returns>
        public static PurchaseOrderItem Create(PurchaseOrder parent, Article article, Quantity quantity, string? description) => new(parent, article, quantity, description);
    }
}

namespace ModularMonolith.Modules.Ordering.Domain.Orders
{
    using System.Linq;

    /// <summary>
    /// Represents an abstract base class for order items.
    /// </summary>
    public abstract class OrderItem : DomainEntity<OrderItemId, int>
    {
        /// <summary>
        /// Gets the position of the order item.
        /// </summary>
        internal short Position { get; private set; }

        /// <summary>
        /// Gets the article of the order item.
        /// </summary>
        internal ArticleId ArticleId { get; private set; }

        /// <summary>
        /// Gets the quantity of the order item.
        /// </summary>
        internal Quantity Quantity { get; private set; }

        /// <summary>
        /// Gets the order associated with the item.
        /// </summary>
        internal Order Order { get; private set; }

        /// <summary>
        /// Gets the description of the order item.
        /// </summary>
        internal string? Description { get; private set; }

        /// <summary>
        /// Gets the completed quantity of the order item.
        /// </summary>
        internal decimal? QuantityCompleted { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderItem"/> class.
        /// </summary>
        /// <param name="order">The order associated with the item.</param>
        /// <param name="article">The article of the item.</param>
        /// <param name="quantity">The quantity of the item.</param>
        /// <param name="description"></param>
        protected OrderItem(Order order, Article article, Quantity quantity, string? description)
        {
            Order = order;
            ArticleId = article;
            Quantity = quantity;
            Description = description;

            short maxPosition = order.Items.Max(n => (short?)n.Position) ?? 0;
            Position = ++maxPosition;
        }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        /// <summary>
        /// Initializes a new instance of the <see cref="OrderItem"/> class.
        /// </summary>
        protected OrderItem()
        {
        }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        /// <summary>
        /// Updates the article and quantity of the order item.
        /// </summary>
        /// <param name="articleId">The new article of the item.</param>
        /// <param name="quantity">The new quantity of the item.</param>
        /// <param name="description">The new description of the item.</param>
        internal void Update(ArticleId articleId, Quantity quantity, string? description)
        {
            ArticleId = articleId;
            Quantity = quantity;
            Description = description;
        }

        /// <summary>
        /// Removes the order item.
        /// </summary>
        internal void Remove()
        {
            short counter = 0;
            var items = Order.Items.Where(n => n.Id != Id).OrderBy(n => n.Position).ToList();
            items.ForEach(n =>
            {
                n.Position = ++counter;
            });
        }
    }
}

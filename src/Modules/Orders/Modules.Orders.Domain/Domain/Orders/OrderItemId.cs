namespace ModularMonolith.Modules.Orders.Domain.Orders
{
    /// <summary>
    /// Represents the unique identifier for an order item.
    /// </summary>
    public record OrderItemId : EntityId<int>
    {
        private OrderItemId(int id) : base(id)
        {
        }

        /// <summary>
        /// Implicitly converts an integer value to an OrderItemId.
        /// </summary>
        /// <param name="value">The integer value to convert.</param>
        /// <returns>The converted OrderItemId.</returns>
        public static implicit operator OrderItemId(int value) => new(value);

        /// <summary>
        /// Implicitly converts an OrderItemId to an integer value.
        /// </summary>
        /// <param name="value">The OrderItemId to convert.</param>
        /// <returns>The converted integer value.</returns>
        public static implicit operator int(OrderItemId value) => value.Id;
    }
}

namespace ModularMonolith.Modules.Orders.Domain.Orders
{
    /// <summary>
    /// Represents the unique identifier for an order.
    /// </summary>
    public record OrderId : EntityId<int>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OrderId"/> class.
        /// </summary>
        /// <param name="id">The identifier value.</param>
        private OrderId(int id) : base(id)
        {
        }

        /// <summary>
        /// Implicitly converts an integer value to an <see cref="OrderId"/>.
        /// </summary>
        /// <param name="value">The integer value to convert.</param>
        /// <returns>The converted <see cref="OrderId"/>.</returns>
        public static implicit operator OrderId(int value) => new(value);

        /// <summary>
        /// Implicitly converts an <see cref="OrderId"/> to an integer value.
        /// </summary>
        /// <param name="value">The <see cref="OrderId"/> to convert.</param>
        /// <returns>The converted integer value.</returns>
        public static implicit operator int(OrderId value) => value.Id;
    }
}

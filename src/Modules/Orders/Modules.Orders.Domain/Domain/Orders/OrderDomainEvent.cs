namespace ModularMonolith.Modules.Orders.Domain.Orders
{
    /// <summary>
    /// Represents an abstract base class for order domain events.
    /// </summary>
    public abstract record OrderDomainEvent : IDomainEvent
    {
        private readonly Order entity;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderDomainEvent"/> class.
        /// </summary>
        /// <param name="entity">The order entity associated with the domain event.</param>
        public OrderDomainEvent(Order entity)
        {
            this.entity = entity;
        }

        /// <summary>
        /// Gets the order type as a byte.
        /// </summary>
        public byte OrderType => (byte)entity.OrderType.Key;

        /// <summary>
        /// Gets the order ID.
        /// </summary>
        public OrderId OrderId => entity.Id;
    }
}

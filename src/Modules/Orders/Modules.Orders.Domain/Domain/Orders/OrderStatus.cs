namespace ModularMonolith.Modules.Orders.Domain.Orders
{
    /// <summary>
    /// Represents the state of an order.
    /// </summary>
    public class OrderStatus : Enumeration<byte>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OrderStatus"/> class.
        /// </summary>
        /// <param name="id">The ID of the order state.</param>
        /// <param name="name">The name of the order state.</param>
        private OrderStatus(byte id, string name) : base(id, name)
        {
        }

        /// <summary>
        /// Gets the unconfirmed order state.
        /// </summary>
        public static OrderStatus Unconfirmed => new(0, "Niepotwierdzony");

        /// <summary>
        /// Gets the confirmed order state.
        /// </summary>
        public static OrderStatus Confirmed => new(1, "Potwierdzony");


        /// <summary>
        /// 
        /// </summary>
        public static OrderStatus InProgress => new(2, "W realizacji");

        /// <summary>
        /// 
        /// </summary>
        public static OrderStatus Completed => new(3, "Zrealizowany");

        /// <summary>
        /// 
        /// </summary>
        public static OrderStatus Finished => new(4, "Zakończony");
    }
}

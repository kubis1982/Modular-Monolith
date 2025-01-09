namespace ModularMonolith.Modules.Ordering.Domain.Orders
{
    using System;

    /// <summary>
    /// Represents a purchase order.
    /// </summary>
    public sealed class PurchaseOrder : Order
    {
        private PurchaseOrder()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PurchaseOrder"/> class.
        /// </summary>
        /// <param name="contractor">The contractor associated with the purchase order.</param>
        /// <param name="warehouse">The warehouse associated with the purchase order.</param>
        /// <param name="executionDate">The execution date of the purchase order.</param>
        /// <param name="orderNo">The order number of the purchase order.</param>
        /// <param name="description"></param>
        /// <param name="address"></param>
        private PurchaseOrder(Contractor contractor, Warehouse warehouse, DateTime executionDate, OrderNumber orderNo, string? description, Address address) : base(contractor, warehouse, executionDate, orderNo, description, address)
        {
        }

        /// <summary>
        /// Creates a new instance of the <see cref="PurchaseOrder"/> class.
        /// </summary>
        /// <param name="contractor">The contractor associated with the purchase order.</param>
        /// <param name="warehouse">The warehouse associated with the purchase order.</param>
        /// <param name="executionDate">The execution date of the purchase order.</param>
        /// <param name="orderNo">The order number of the purchase order.</param>
        /// <param name="description"></param>
        /// <param name="address"></param>
        /// <returns>A new instance of the <see cref="PurchaseOrder"/> class.</returns>
        public static PurchaseOrder Create(Contractor contractor, Warehouse warehouse, DateTime executionDate, OrderNumber orderNo, string? description, Address address) => new(contractor, warehouse, executionDate, orderNo, description, address);

        /// <summary>
        /// Creates a new order item for the purchase order.
        /// </summary>
        /// <param name="article">The article associated with the order item.</param>
        /// <param name="quantity">The quantity of the order item.</param>
        /// <param name="description"></param>
        /// <returns>A new instance of the <see cref="OrderItem"/> class.</returns>
        protected override OrderItem CreateItem(Article article, Quantity quantity, string? description) => PurchaseOrderItem.Create(this, article, quantity, description);
    }
}

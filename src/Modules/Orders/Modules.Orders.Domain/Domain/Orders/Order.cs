namespace ModularMonolith.Modules.Orders.Domain.Orders
{
    using ModularMonolith.Modules.Orders.Domain.Orders.Exceptions;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Represents an abstract base class for orders.
    /// </summary>
    public abstract class Order : DomainEntity<OrderId, int>, IAggregateRoot
    {
        private readonly List<OrderItem> items = [];
        internal ContractorId ContractorId { get; private set; }
        internal WarehouseId WarehouseId { get; private set; }
        internal DateTime ExecutionDate { get; private set; }
        internal OrderStatus Status { get; private set; }
        internal OrderNumber OrderNo { get; private set; }
        internal OrderType OrderType { get; private set; }
        internal string? Description { get; private set; }
        internal Address? Address { get; private set; }

        internal IReadOnlyCollection<OrderItem> Items => items;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        protected Order()
        {
        }

#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        /// <summary>
        /// Initializes a new instance of the <see cref="Order"/> class.
        /// </summary>
        /// <param name="contractor">The contractor associated with the order.</param>
        /// <param name="warehouse">The warehouse associated with the order.</param>
        /// <param name="executionDate">The execution date of the order.</param>
        /// <param name="orderNo">The order number.</param>
        /// <param name="description"></param>
        /// <param name="address"></param>
        protected Order(Contractor contractor, Warehouse warehouse, DateTime executionDate, OrderNumber orderNo, string? description, Address address)
        {
            if (contractor.IsBlocked)
            {
                throw new ContractorBlockedException(contractor);
            }
            if (warehouse.IsBlocked)
            {
                throw new WarehouseBlockedException(warehouse);
            }
            OrderType = OrderType.GetOrderType(GetType());
            ContractorId = contractor;
            WarehouseId = warehouse;
            ExecutionDate = executionDate;
            Status = OrderStatus.Unconfirmed;
            OrderNo = orderNo;
            Description = description;
            Address = address;
            AddEvent(new Events.OrderCreatedEvent(this, ContractorId, WarehouseId, ExecutionDate, Status, OrderNo, description, address));
        }

        /// <summary>
        /// Updates the contractor, warehouse, and execution date of the order.
        /// </summary>
        /// <param name="contractor">The updated contractor.</param>
        /// <param name="warehouse">The updated warehouse.</param>
        /// <param name="executionDate">The updated execution date.</param>
        /// <param name="description"></param>
        /// <param name="address"></param>
        public void Update(Contractor contractor, Warehouse warehouse, DateTime executionDate, string? description, Address address)
        {
            if (Status > OrderStatus.Unconfirmed)
            {
                throw new CannotUpdateOrderException();
            }
            ContractorId = contractor;
            WarehouseId = warehouse;
            ExecutionDate = executionDate;
            Description = description;
            Address = address;
            AddEvent(new Events.OrderUpdatedEvent(this, ContractorId, WarehouseId, ExecutionDate, description, address));
        }

        /// <summary>
        /// Removes the order.
        /// </summary>
        public void Remove()
        {
            if (Status > OrderStatus.Unconfirmed)
            {
                throw new CannotUpdateOrderException();
            }
            AddEvent(new Events.OrderRemovedEvent(this));
        }

        /// <summary>
        /// Adds an item to the order.
        /// </summary>
        /// <param name="article">The article of the item.</param>
        /// <param name="quantity">The quantity of the item.</param>
        /// <param name="description"></param>
        /// <returns>The added order item.</returns>
        public OrderItem AddItem(Article article, Quantity quantity, string? description)
        {
            if (Status > OrderStatus.Unconfirmed)
            {
                throw new CannotUpdateOrderException();
            }
            if (article.IsBlocked)
            {
                throw new ArticleBlockedException(article);
            }
            OrderItem orderItem = CreateItem(article, quantity, description);
            items.Add(orderItem);
            AddEvent(new Events.OrderItemCreatedEvent(this, orderItem, article.Id, quantity, description));
            return orderItem;
        }

        /// <summary>
        /// Updates an item in the order.
        /// </summary>
        /// <param name="orderItemId">The ID of the order item to update.</param>
        /// <param name="article">The updated article.</param>
        /// <param name="quantity">The updated quantity.</param>
        /// <param name="description"></param>
        public void UpdateItem(OrderItemId orderItemId, Article article, Quantity quantity, string? description)
        {
            if (Status > OrderStatus.Unconfirmed)
            {
                throw new CannotUpdateOrderException();
            }
            OrderItem orderItem = items.SingleOrDefault(n => n.Id == orderItemId)
                ?? throw new OrderItemNotFoundException(orderItemId);
            orderItem.Update(article, quantity, description);
            AddEvent(new Events.OrderItemUpdatedEvent(this, orderItemId, article.Id, quantity, description));
        }

        /// <summary>
        /// Removes an item from the order.
        /// </summary>
        /// <param name="orderItemId">The ID of the order item to remove.</param>
        public void RemoveItem(OrderItemId orderItemId)
        {
            if (Status > OrderStatus.Unconfirmed)
            {
                throw new CannotUpdateOrderException();
            }
            OrderItem? orderItem = items.SingleOrDefault(n => n.Id == orderItemId);
            if (orderItem != null)
            {
                orderItem.Remove();
                items.Remove(orderItem);
                AddEvent(new Events.OrderItemRemovedEvent(this, orderItemId));
            }
        }

        /// <summary>
        /// Confirms the order.
        /// </summary>
        public void Confirm()
        {
            if (Items.Count == 0)
            {
                throw new OrderConfirmationWithoutItemsException();
            }
            Status = OrderStatus.Confirmed;
            AddEvent(new Events.OrderConfirmedEvent(this));
        }

        /// <summary>
        /// Creates an order item.
        /// </summary>
        /// <param name="article">The article of the item.</param>
        /// <param name="quantity">The quantity of the item.</param>
        /// <param name="description"></param>
        /// <returns>The created order item.</returns>
        protected abstract OrderItem CreateItem(Article article, Quantity quantity, string? description);
    }
}

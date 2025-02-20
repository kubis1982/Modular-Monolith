﻿namespace ModularMonolith.Modules.Ordering.Customizations
{

    using AutoFixture;
    using ModularMonolith.Modules.Ordering.Domain;
    using ModularMonolith.Modules.Ordering.Domain.Orders;
    using ModularMonolith.Modules.Ordering.Persistance.ReadModel;
    using ModularMonolith.Modules.Ordering.Requests.Orders;

    internal class OrdersCustomisation : ICustomization
    {

        public void Customize(IFixture fixture)
        {

            fixture.Customize<OrderEntity>(n => n.With(m => m.TypeId, EntityType.PurchaseOrder.Code.Value)
                .With(m => m.Status, OrderStatus.Unconfirmed.Key));

            fixture.Customize<OrderItemEntity>(n => n.With(m => m.TypeId, EntityType.PurchaseOrderItem.Code.Value)
                .With(n => n.QuantityUnit, "kg")
                .With(n => n.QuantityDenominator, 1)
                .With(n => n.QuantityNumerator, 1));

            fixture.Customize<CreatePurchaseItemRequest>(n => n
                .With(n => n.Unit, "kg")
                .With(n => n.Denominator, 1)
                .With(n => n.Numerator, 1));

            fixture.Customize<UpdatePurchaseItemRequest>(n => n
                .With(n => n.Unit, "kg")
                .With(n => n.Denominator, 1)
                .With(n => n.Numerator, 1));

            fixture.Customize<CreateSalesItemRequest>(n => n
                .With(n => n.Unit, "kg")
                .With(n => n.Denominator, 1)
                .With(n => n.Numerator, 1));

            fixture.Customize<UpdateSalesItemRequest>(n => n
                .With(n => n.Unit, "kg")
                .With(n => n.Denominator, 1)
                .With(n => n.Numerator, 1));
        }
    }
}

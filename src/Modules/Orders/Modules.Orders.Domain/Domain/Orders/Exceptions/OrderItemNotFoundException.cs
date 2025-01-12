﻿namespace ModularMonolith.Modules.Orders.Domain.Orders.Exceptions
{
    internal class OrderItemNotFoundException : AppException
    {
        public OrderItemNotFoundException(OrderItemId itemId) : base($"Pozycja '{itemId.Id}' nie jest zarejestrowana dla tego zamówienia zakupu")
        {
        }
    }
}

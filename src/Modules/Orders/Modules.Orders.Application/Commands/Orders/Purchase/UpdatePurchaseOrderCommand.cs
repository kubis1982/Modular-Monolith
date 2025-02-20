﻿namespace ModularMonolith.Modules.Ordering.Commands.Orders.Purchase
{
    using ModularMonolith.Modules.Ordering.Domain;
    using ModularMonolith.Modules.Ordering.Domain.Orders;
    using ModularMonolith.Modules.Ordering.Services;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public sealed record UpdatePurchaseOrderCommand(int OrderId) : UnitOfWorkCommand
    {
        public required int ContractorId { get; set; }
        public required int WarehouseId { get; set; }
        public required DateTime ExecutionDate { get; set; }
        public required string? Description { get; set; }
        public required string? AddressLine1 { get; set; }
        public required string? AddressLine2 { get; set; }
        public required string? AddressCity { get; set; }
        public required string? AddressPostalCode { get; set; }
        public required string? AddressCountry { get; set; }

        internal class UpdatePurchaseOrderCommandHandler(IOrderRepository orderRepository, IContractorsService contractorsService, IWarehousesService warehousesService) : UnitOfWorkCommandHandler<UpdatePurchaseOrderCommand>
        {
            public override async Task Handle(UpdatePurchaseOrderCommand command, CancellationToken cancellationToken)
            {
                Order order = await orderRepository.SingleAsync(OrderSpec.ById(OrderType.Purchase, command.OrderId), cancellationToken);
                Contractor contractor = await contractorsService.GetContractorAsync(command.ContractorId, cancellationToken);
                Warehouse warehouse = await warehousesService.GetWarehouseAsync(command.WarehouseId, cancellationToken);
                Address address = Address.Create(command.AddressLine1, command.AddressLine2, command.AddressPostalCode, command.AddressCity, command.AddressCountry);
                order.Update(contractor, warehouse, command.ExecutionDate, command.Description, address);
            }
        }
    }
}

﻿namespace ModularMonolith.Modules.Ordering.Commands.Orders.Purchase
{
    using ModularMonolith.Modules.Ordering.Domain;
    using ModularMonolith.Modules.Ordering.Domain.Orders;
    using ModularMonolith.Modules.Ordering.Extensions;
    using ModularMonolith.Modules.Ordering.Services;
    using ModularMonolith.Shared.Time;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public sealed record CreatePurchaseOrderCommand : EntityCommand
    {
        public required int ContractorId { get; set; }
        public required int WarehouseId { get; set; }
        public required DateTime ExecutionDate { get; set; }
        public string? Description { get; set; }
        public string? AddressLine1 { get; set; }
        public string? AddressLine2 { get; set; }
        public string? AddressCity { get; set; }
        public string? AddressPostalCode { get; set; }
        public string? AddressCountry { get; set; }

        internal class CreatePurchaseOrderCommandHandler(IOrderRepository orderRepository, IContractorsService contractorsService, IWarehousesService warehousesService, IDocumentNumberGenerator documentNumberGenerator, IClock clock) : EntityCommandHandler<CreatePurchaseOrderCommand>
        {
            public override async Task<EntityIdentityResult> Handle(CreatePurchaseOrderCommand command, CancellationToken cancellationToken)
            {
                Contractor contractor = await contractorsService.GetContractorAsync(command.ContractorId, cancellationToken);
                Warehouse warehouse = await warehousesService.GetWarehouseAsync(command.WarehouseId, cancellationToken);
                string documenNumber = await documentNumberGenerator.CreateDocumentNumberAsync(EntityType.PurchaseOrder, clock.Now, cancellationToken);
                Address address = Address.Create(command.AddressLine1, command.AddressLine2, command.AddressPostalCode, command.AddressCity, command.AddressCountry);
                PurchaseOrder order = PurchaseOrder.Create(contractor, warehouse, command.ExecutionDate, documenNumber, command.Description, address);
                await orderRepository.AddAsync(order, cancellationToken);
                return order.CreateEntityIdentityResult();
            }
        }
    }
}

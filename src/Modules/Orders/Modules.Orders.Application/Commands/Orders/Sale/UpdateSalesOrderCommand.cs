namespace ModularMonolith.Modules.Orders.Commands.Orders.Sale
{
    using ModularMonolith.Modules.Orders.Domain;
    using ModularMonolith.Modules.Orders.Domain.Orders;
    using ModularMonolith.Modules.Orders.Services;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public sealed record UpdateSalesOrderCommand(int OrderId) : UnitOfWorkCommand
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
        internal class UpdateSalesOrderCommandHandler(IOrderRepository orderRepository, IContractorsService contractorsService, IWarehousesService warehousesService) : UnitOfWorkCommandHandler<UpdateSalesOrderCommand>
        {
            public override async Task Handle(UpdateSalesOrderCommand command, CancellationToken cancellationToken)
            {
                Order order = await orderRepository.SingleAsync(OrderSpec.ById(OrderType.Sale, command.OrderId), cancellationToken);
                Contractor contractor = await contractorsService.GetContractorAsync(command.ContractorId, cancellationToken);
                Warehouse warehouse = await warehousesService.GetWarehouseAsync(command.WarehouseId, cancellationToken);
                Address address = Address.Create(command.AddressLine1, command.AddressLine2, command.AddressPostalCode, command.AddressCity, command.AddressCountry);
                order.Update(contractor, warehouse, command.ExecutionDate, command.Description, address);
            }
        }
    }
}

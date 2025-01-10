namespace ModularMonolith.Modules.Warehouses.Commands.Warehouses
{
    using System.Threading;
    using System.Threading.Tasks;

    public sealed record DeleteWarehouseCommand(int WarehouseId) : UnitOfWorkCommand
    {

        internal class DeleteWarehouseCommandHandler(IWarehouseRepository repository, IWarehouseUsageService warehouseUsageService) : UnitOfWorkCommandHandler<DeleteWarehouseCommand>
        {
            public override async Task Handle(DeleteWarehouseCommand command, CancellationToken cancellationToken)
            {
                var warehouse = await repository.SingleAsync(WarehouseSpec.ById(command.WarehouseId), cancellationToken);
                warehouse.Remove(warehouseUsageService);
                await repository.DeleteAsync(warehouse, cancellationToken);
            }
        }
    }
}

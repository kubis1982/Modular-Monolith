namespace ModularMonolith.Modules.Warehouses.Commands.Warehouses
{
    using System.Threading;
    using System.Threading.Tasks;

    public sealed record UnblockWarehouseCommand(int WarehouseId) : UnitOfWorkCommand
    {
        internal class UnblockWarehouseCommandHandler(IWarehouseRepository repository) : UnitOfWorkCommandHandler<UnblockWarehouseCommand>
        {
            public override async Task Handle(UnblockWarehouseCommand command, CancellationToken cancellationToken)
            {
                var warehouse = await repository.SingleAsync(WarehouseSpec.ById(command.WarehouseId), cancellationToken);
                warehouse.Unblock();
            }
        }
    }
}

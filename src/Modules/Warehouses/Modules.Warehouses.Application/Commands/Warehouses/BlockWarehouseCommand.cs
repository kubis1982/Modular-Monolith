namespace ModularMonolith.Modules.Warehouses.Commands.Warehouses
{
    using System.Threading;
    using System.Threading.Tasks;

    public sealed record BlockWarehouseCommand(int WarehouseId) : UnitOfWorkCommand
    {
        internal class BlockWarehouseCommandHandler(IWarehouseRepository repository) : UnitOfWorkCommandHandler<BlockWarehouseCommand>
        {
            public override async Task Handle(BlockWarehouseCommand command, CancellationToken cancellationToken)
            {
                var warehouse = await repository.SingleAsync(WarehouseSpec.ById(command.WarehouseId), cancellationToken);
                warehouse.Block();
            }
        }
    }
}

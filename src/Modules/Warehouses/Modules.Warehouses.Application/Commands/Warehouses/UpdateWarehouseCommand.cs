namespace ModularMonolith.Modules.Warehouses.Commands.Warehouses
{
    using System.Threading;
    using System.Threading.Tasks;

    public sealed record UpdateWarehouseCommand(int WarehouseId, string Code, string Name) : UnitOfWorkCommand
    {
        public string? Description { get; init; }
        internal class UpdateWarehouseCommandHandler(IWarehouseRepository repository) : UnitOfWorkCommandHandler<UpdateWarehouseCommand>
        {
            public override async Task Handle(UpdateWarehouseCommand command, CancellationToken cancellationToken)
            {
                var warehouse = await repository.SingleAsync(WarehouseSpec.ById(command.WarehouseId), cancellationToken);
                warehouse.Update(command.Code, command.Name, command.Description);
            }
        }
    }
}

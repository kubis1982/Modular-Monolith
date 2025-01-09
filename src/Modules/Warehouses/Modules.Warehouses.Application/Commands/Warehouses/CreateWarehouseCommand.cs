namespace ModularMonolith.Modules.Warehouses.Commands.Warehouses
{
    using System.Threading;
    using System.Threading.Tasks;

    public sealed record CreateWarehouseCommand(string Code, string Name) : EntityCommand
    {
        public string? Description { get; init; }
        internal class CreateWarehouseCommandHandler(IWarehouseRepository repository) : EntityCommandHandler<CreateWarehouseCommand>
        {
            public override async Task<EntityIdentityResult> Handle(CreateWarehouseCommand command, CancellationToken cancellationToken)
            {
                var warehouse = Warehouse.Create(command.Code, command.Name, command.Description);
                await repository.AddAsync(warehouse, cancellationToken);
                return EntityIdentityResult.Create(warehouse);
            }
        }
    }
}

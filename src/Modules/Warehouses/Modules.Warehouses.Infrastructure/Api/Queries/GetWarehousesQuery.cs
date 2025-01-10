namespace ModularMonolith.Modules.Warehouses.Api.Queries
{
    using ModularMonolith.Modules.Warehouses.Api;
    using ModularMonolith.Modules.Warehouses.Persistance.ReadModel;
    using ModularMonolith.Shared.CQRS.Queries;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    internal sealed record GetWarehousesQuery(int[] WarehouseIds) : Query<WarehouseDto[]>
    {
        internal class GetWarehousesQueryHandler(ReadDbContext db) : QueryHandler<GetWarehousesQuery, WarehouseDto[]>
        {
            public override async Task<WarehouseDto[]> Handle(GetWarehousesQuery query, CancellationToken cancellationToken)
            {
                return await (from warehouses in db.Warehouses.Where(x => query.WarehouseIds.Contains(x.Id))
                              select new WarehouseDto
                              {
                                  TypeId = warehouses.TypeId,
                                  Id = warehouses.Id,
                                  Code = warehouses.Code,
                                  Name = warehouses.Name,
                                  IsBlocked = warehouses.IsBlocked
                              }).ToArrayAsync(cancellationToken: cancellationToken);
            }
        }
    }
}

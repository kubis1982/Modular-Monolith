namespace ModularMonolith.Modules.ReadModel.Queries.Warehouses {
    using Microsoft.EntityFrameworkCore;
    using ModularMonolith.Modules.ReadModel.Persistance.ReadModel;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    public sealed record GetWarehouseQuery(int WarehouseId) : Query<GetWarehouseQueryResult?> {
        internal class GetWarehouseQueryHandler(ReadDbContext dbContext) : QueryHandler<GetWarehouseQuery, GetWarehouseQueryResult?> {
            public override async Task<GetWarehouseQueryResult?> Handle(GetWarehouseQuery query, CancellationToken cancellationToken) {
                var result = (
                    from warehouses in dbContext.Warehouses.Where(n => n.Id == query.WarehouseId)
                    select new GetWarehouseQueryResult {
                        TypeId = warehouses.TypeId,
                        Id = warehouses.Id,
                        Code = warehouses.Code,
                        Name = warehouses.Name,
                        Description = warehouses.Description,
                        IsBlocked = warehouses.IsBlocked
                    }
                );
                return await result.SingleOrDefaultAsync(cancellationToken);
            }
        }
    }

    public class GetWarehouseQueryResult {
        public required string TypeId { get; set; }
        public required int Id { get; set; }
        public required string Code { get; set; }
        public required string Name { get; set; }
        public required string? Description { get; set; }
        public required bool IsBlocked { get; set; }
    }
}

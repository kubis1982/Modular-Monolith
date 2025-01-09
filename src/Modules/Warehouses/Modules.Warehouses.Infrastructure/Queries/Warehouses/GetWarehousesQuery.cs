namespace ModularMonolith.Modules.Warehouses.Queries.Warehoues
{
    using ModularMonolith.Modules.Warehouses.Persistance.ReadModel;
    using ModularMonolith.Modules.Warehouses.Persistance.ReadModel.Entities;
    using ModularMonolith.Shared.CQRS.Queries;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading;
    using System.Threading.Tasks;

    public sealed record GetWarehousesQuery : Query<IEnumerable<GetWarehousesQueryResult>>
    {
        public Expression<Func<WarehouseEntity, bool>> Expression { get; }

        private GetWarehousesQuery(Expression<Func<WarehouseEntity, bool>> expression)
        {
            Expression = expression;
        }

        /// <summary>
        /// Gets all warehouses
        /// </summary>
        public GetWarehousesQuery(bool includeBlocked) : this(x => x.IsBlocked == (includeBlocked ? x.IsBlocked : false)) { }

        /// <summary>
        /// Gets warehouse by id
        /// </summary>
        /// <param name="warehouseId"></param>
        public GetWarehousesQuery(int warehouseId) : this(x => x.Id == warehouseId) { }

        /// <summary>
        /// Gets warehouse by code
        /// </summary>
        /// <param name="code"></param>
        public GetWarehousesQuery(string code) : this(x => x.Code == code) { }

        internal class GetWarehousesQueryHandler(ReadDbContext dbContext) : QueryHandler<GetWarehousesQuery, IEnumerable<GetWarehousesQueryResult>>
        {
            public override async Task<IEnumerable<GetWarehousesQueryResult>> Handle(GetWarehousesQuery request, CancellationToken cancellationToken)
            {
                return await dbContext.Warehouses.Where(request.Expression).Select(x => new GetWarehousesQueryResult
                {
                    Code = x.Code,
                    Name = x.Name,
                    Id = x.Id,
                    Description = x.Description,
                    IsBlocked = x.IsBlocked
                }).OrderBy(x => x.Name).ToArrayAsync();
            }
        }
    }

    public class GetWarehousesQueryResult
    {
        public int Id { get; set; }
        public required string Code { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public bool IsBlocked { get; set; }
    }
}

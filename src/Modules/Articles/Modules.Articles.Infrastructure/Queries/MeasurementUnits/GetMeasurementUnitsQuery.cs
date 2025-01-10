namespace ModularMonolith.Modules.Articles.Queries.MeasurementUnits
{
    using ModularMonolith.Modules.Articles.Persistance.ReadModel;
    using ModularMonolith.Modules.Articles.Persistance.ReadModel.Entities;
    using ModularMonolith.Shared.CQRS.Queries;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading;
    using System.Threading.Tasks;

    public sealed record GetMeasurementUnitsQuery : Query<IEnumerable<GetMeasurementUnitsQueryResult>>
    {

        private GetMeasurementUnitsQuery(Expression<Func<MeasurementUnitEntity, bool>> expression)
        {
            Expression = expression;
        }

        /// <summary>
        /// Gets all measurement units
        /// </summary>
        public GetMeasurementUnitsQuery() : this(n => true) { }

        /// <summary>
        /// Gets a measurement unit by id
        /// </summary>
        /// <param name="measurementUnitId"></param>
        public GetMeasurementUnitsQuery(int measurementUnitId) : this(n => n.Id == measurementUnitId) { }

        /// <summary>
        /// Gets a measurement unit by name
        /// </summary>
        /// <param name="code"></param>
        public GetMeasurementUnitsQuery(string code) : this(n => n.Code == code) { }

        public Expression<Func<MeasurementUnitEntity, bool>> Expression { get; }

        internal class GetMeasurementUnitsQueryHandler(ReadDbContext dbContext) : QueryHandler<GetMeasurementUnitsQuery, IEnumerable<GetMeasurementUnitsQueryResult>>
        {
            private readonly ReadDbContext dbContext = dbContext;

            public override async Task<IEnumerable<GetMeasurementUnitsQueryResult>> Handle(GetMeasurementUnitsQuery request, CancellationToken cancellationToken)
            {
                return await dbContext.MeasurementUnits.Where(request.Expression).Select(n => new GetMeasurementUnitsQueryResult
                {
                    Id = n.Id,
                    Code = n.Code,
                    Name = n.Name,
                }).OrderBy(n => n.Name).ToArrayAsync(cancellationToken: cancellationToken);
            }
        }
    }

    public class GetMeasurementUnitsQueryResult
    {
        public int Id { get; set; }
        public required string Code { get; set; }
        public required string Name { get; set; }
    }
}

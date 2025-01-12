namespace ModularMonolith.Modules.Orders.Queries.Orders
{
    using ModularMonolith.Modules.Orders.Domain.Orders;
    using ModularMonolith.Shared.CQRS.Queries;
    using ModularMonolith.Shared.Kernel.Types;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Get all order statuses
    /// </summary>
    public sealed record GetOrderStatesQuery : Query<IEnumerable<GetOrderStatesQueryResult>>
    {
        private GetOrderStatesQuery(Expression<Func<OrderStatus, bool>> expression)
        {
            Expression = expression;
        }

        /// <summary>
        /// Gets all order states
        /// </summary>
        public GetOrderStatesQuery() : this(n => true) { }

        /// <summary>
        /// Gets a order state by state no
        /// </summary>
        /// <param name="state"></param>
        public GetOrderStatesQuery(byte state) : this(n => n.Key == state) { }

        public Expression<Func<OrderStatus, bool>> Expression { get; }

        internal class GetOrderStatesQueryHandler : QueryHandler<GetOrderStatesQuery, IEnumerable<GetOrderStatesQueryResult>>
        {
            public override Task<IEnumerable<GetOrderStatesQueryResult>> Handle(GetOrderStatesQuery request, CancellationToken cancellationToken)
            {
                return Task.FromResult(Enumeration<byte>.GetAll<OrderStatus>().Select(n => new GetOrderStatesQueryResult
                {
                    Id = n.Key,
                    Name = n.Name
                }));
            }
        }
    }

    public class GetOrderStatesQueryResult
    {
        public byte Id { get; set; }
        public required string Name { get; set; }
    }
}


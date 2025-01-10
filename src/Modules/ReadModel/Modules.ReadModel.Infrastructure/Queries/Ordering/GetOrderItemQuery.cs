namespace ModularMonolith.Modules.ReadModel.Queries.Ordering {
    using Microsoft.EntityFrameworkCore;
    using ModularMonolith.Modules.ReadModel.Persistance.ReadModel;
    using ModularMonolith.Modules.ReadModel.Persistance.ReadModel.Ordering;
    using ModularMonolith.Modules.ReadModel.Queries;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    public sealed record GetOrderItemQuery(OrderType OrderType, int OrderId, int OrderItemId) : Query<GetOrderItemQueryResult?> {
        internal class GetDeliveryItemQueryHandler(ReadDbContext dbContext) : QueryHandler<GetOrderItemQuery, GetOrderItemQueryResult?> {
            public override async Task<GetOrderItemQueryResult?> Handle(GetOrderItemQuery query, CancellationToken cancellationToken) {
                var result = 
                    from orderItem in dbContext.OrderItems.Where(n => n.Id == query.OrderItemId && n.Order.OrderType == (byte)query.OrderType && n.Order.Id == query.OrderId) 
                    select new GetOrderItemQueryResult {
                        TypeId = orderItem.TypeId,
                        Id = orderItem.Id,
                        Description = orderItem.Description,
                        Article = new ArticleResult {
                            TypeId = orderItem.Article.TypeId,
                            Id = orderItem.Article.Id,
                            Code = orderItem.Article.Code,
                            Name = orderItem.Article.Name,
                            Unit = orderItem.Article.Unit
                        },
                        Order = new DocumentResult {
                            TypeId = orderItem.Order.TypeId,
                            Id = orderItem.Order.Id,
                            Number = orderItem.Order.OrderNo,
                        },
                        Quantity = orderItem.Quantity,
                        QuantityUnit = orderItem.QuantityUnit,
                        QuantityCompleted = orderItem.QuantityCompleted
                    }
                ;
                return await result.SingleOrDefaultAsync(cancellationToken);
            }
        }
    }

    public class GetOrderItemQueryResult : GetOrderItemResult {
        public required string QuantityUnit { get; set; }
        public required DocumentResult Order { get; set; }
    }
}
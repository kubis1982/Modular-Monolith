namespace ModularMonolith.Modules.ReadModel.Queries.Orders {
    using Microsoft.EntityFrameworkCore;
    using ModularMonolith.Modules.ReadModel.Persistance.ReadModel;
    using ModularMonolith.Modules.ReadModel.Persistance.ReadModel.Ordering;
    using ModularMonolith.Modules.ReadModel.Queries;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    public sealed record GetOrderQuery(OrderType OrderType, int OrderId) : Query<GetOrderQueryResult?> {
        internal class GetOrderQueryHandler(ReadDbContext dbContext) : QueryHandler<GetOrderQuery, GetOrderQueryResult?> {
            public override async Task<GetOrderQueryResult?> Handle(GetOrderQuery query, CancellationToken cancellationToken) {
                var result = 
                    from order in dbContext.Orders.Where(n => n.OrderType == (byte)query.OrderType && n.Id == query.OrderId)
                    select new GetOrderQueryResult {
                        TypeId = order.TypeId,
                        Id = order.Id,
                        ExecutionDate = order.ExecutionDate,
                        OrderNo = order.OrderNo,
                        Description = order.Description,
                        Contractor = new EntityResult {
                            TypeId = order.Contractor.TypeId,
                            Id = order.Contractor.Id,
                            Code = order.Contractor.Code,
                            Name = order.Contractor.Name
                        },
                        Warehouse = new EntityResult {
                            TypeId = order.Warehouse.TypeId,
                            Id = order.Warehouse.Id,
                            Code = order.Warehouse.Code,
                            Name = order.Warehouse.Name
                        },
                        Status = new StatusResult {
                            Id = order.Status,
                            Name = GetStatus(order.Status)
                        },
                        Address = new AddressResult {
                            Line1 = order.AddressLine1,
                            Line2 = order.AddressLine2,
                            PostalCode = order.AddressPostalCode,
                            City = order.AddressCity,
                            Country = order.AddressCountry
                        },
                        Items = (
                            from orderItem in dbContext.OrderItems
                            where orderItem.Order.Id == order.Id
                            select new GetOrderItemResult {
                                TypeId = orderItem.TypeId,
                                Id = orderItem.Id,
                                Quantity = orderItem.Quantity,
                                Description = orderItem.Description,
                                QuantityCompleted = orderItem.QuantityCompleted,
                                Article = new ArticleResult {
                                    TypeId = orderItem.Article.TypeId,
                                    Id = orderItem.Article.Id,
                                    Code = orderItem.Article.Code,
                                    Name = orderItem.Article.Name,
                                    Unit = orderItem.Article.Unit
                                }
                            }
                        ).ToArray()
                    }
                ;
                return await result.SingleOrDefaultAsync(cancellationToken);
            }

            private static Dictionary<int, string> Statues => new() {
                { 0, "Niepotwierdzony" },
                { 1, "Potwierdzony" },
                { 2, "W realizacji" },
                { 3, "Zrealizowany" },
                { 4, "Zakończony" },
                { 5, "Anulowany" }
            };

            private static string GetStatus(byte status) {
                _ = Statues.TryGetValue(status, out string? name);
                return name ?? "Nieokreślony";
            }
        }
    }

    public class GetOrderQueryResult {
        public required string TypeId { get; set; }
        public required int Id { get; set; }
        public DateTime? ExecutionDate { get; set; }
        public string? OrderNo { get; set; }
        public string? Description { get; set; }
        public required EntityResult Contractor { get; set; }
        public required EntityResult Warehouse { get; set; }
        public required StatusResult Status { get; set; }
        public required AddressResult Address { get; set; }
        public required GetOrderItemResult[] Items { get; set; }
    }

    public class GetOrderItemResult {
        public required string TypeId { get; set; }
        public required int Id { get; set; }
        public required decimal Quantity { get; set; }
        public decimal? QuantityCompleted { get; set; }
        public string? Description { get; set; }
        public required ArticleResult Article { get; set; }
    }
}

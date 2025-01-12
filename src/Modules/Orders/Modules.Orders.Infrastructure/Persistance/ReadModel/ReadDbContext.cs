namespace ModularMonolith.Modules.Orders.Persistance.ReadModel
{
    using ModularMonolith.Shared.Persistance.ReadModel;

    public class ReadDbContext(DbContextOptions<ReadDbContext> options) : ReadDbContextBase(options)
    {
        public virtual DbSet<OrderEntity> Orders { get; set; }
        public virtual DbSet<OrderItemEntity> OrderItems { get; set; }
    }
}

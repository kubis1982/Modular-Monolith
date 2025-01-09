namespace ModularMonolith.Modules.Warehouses.Persistance.WriteModel
{
    public class WriteDbContext : WriteDbContextBase
    {
        public WriteDbContext(DbContextOptions<WriteDbContext> options) : base(options)
        {
        }

        protected override string Schema => EntityType.ModuleCode;

        protected override void SeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Warehouse>(n =>
            {
                n.HasData(new
                {
                    WarehouseId.Default.TypeId,
                    Id = WarehouseId.Default,
                    Code = WarehouseCode.Of("MAG"),
                    Name = WarehouseName.Of("Magazyn domyślny"),
                    IsBlocked = false,
                    CreatedBy = 1,
                    HasLocations = false,
                });
            });
        }
    }
}

namespace ModularMonolith.Modules.Articles.Persistance.WriteModel
{
    public class WriteDbContext : WriteDbContextBase
    {
        public WriteDbContext(DbContextOptions<WriteDbContext> options) : base(options)
        {
        }

        protected override string Schema => EntityType.ModuleCode;

        protected override void SeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MeasurementUnit>(n =>
            {
                n.HasData(new
                {
                    TypeId = MeasurementUnitId.Kg.TypeId,
                    Id = MeasurementUnitId.Kg,
                    Code = MeasurementUnitCode.Of("kg"),
                    Name = MeasurementUnitName.Of("kilogram"),
                    CreatedBy = 1
                });
            });
        }
    }
}

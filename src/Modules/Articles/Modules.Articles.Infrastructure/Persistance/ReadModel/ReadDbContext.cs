namespace ModularMonolith.Modules.Articles.Persistance.ReadModel
{
    using ModularMonolith.Modules.Articles.Persistance.ReadModel.Entities;
    using ModularMonolith.Shared.Persistance.ReadModel;

    public class ReadDbContext(DbContextOptions<ReadDbContext> options) : ReadDbContextBase(options)
    {
        public virtual DbSet<ArticleEntity> Articles { get; set; }
        public virtual DbSet<MeasurementUnitEntity> MeasurementUnits { get; set; }

    }
}

namespace ModularMonolith.Modules.Articles.Persistance.WriteModel.Repositories
{
    internal class MeasurementUnitRepository : Repository<MeasurementUnit, MeasurementUnitSpec>, IMeasurementUnitRepository
    {
        public MeasurementUnitRepository(WriteDbContext dbContext) : base(dbContext)
        {
        }
    }
}

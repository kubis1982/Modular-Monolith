namespace ModularMonolith.Modules.Articles.Domain.MeasurementUnits
{
    using ModularMonolith.Shared.Kernel.Types;
    using System;

    public class MeasurementUnitSpec : Specification<MeasurementUnit>, ISingleResultSpecification<MeasurementUnit>
    {
        private MeasurementUnitSpec(Action<ISpecificationBuilder<MeasurementUnit>> action)
            => action(Query);

        public static MeasurementUnitSpec ById(MeasurementUnitId id)
            => new(query => query.Where(n => n.Id == id));
    }
}

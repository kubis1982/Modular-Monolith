namespace ModularMonolith.Modules.Articles.Customizations
{

    using AutoFixture;
    using AutoFixture.Kernel;
    using ModularMonolith.Modules.Articles.Domain.MeasurementUnits;
    using ModularMonolith.Modules.Articles.Persistance.ReadModel.Entities;
    using ModularMonolith.Modules.Articles.Requests.Articles;

    internal class MeasurementUnitCustomization : ICustomization
    {

        public void Customize(IFixture fixture)
        {
            var code = (string)fixture.Create(
                new RegularExpressionRequest(Persistance.MeasurementUnitRestriction.CodePattern),
                new SpecimenContext(fixture));

            fixture.Customize<MeasurementUnitEntity>(n => n
                .With(m => m.TypeId, "STS06")
                .With(m => m.Code, code));

            fixture.Customize<CreateMeasurementUnitRequest>(n => n.With(m => m.Code, (string)fixture.Create(
                    new RegularExpressionRequest(MeasurementUnitCode.Pattern),
                    new SpecimenContext(fixture))));
        }
    }
}

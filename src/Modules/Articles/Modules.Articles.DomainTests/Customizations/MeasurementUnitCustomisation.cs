using ModularMonolith.Modules.Articles.Domain.MeasurementUnits;

namespace ModularMonolith.Modules.Articles.Customizations
{
    internal class MeasurementUnitCustomisation : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.Customize<MeasurementUnit>(n => n.FromFactory(() =>
              MeasurementUnit.Create(fixture.Create<MeasurementUnitCode>(), fixture.Create<string>())
                  .Extensions().SetValue(n => n.Id, new MeasurementUnitId(fixture.Create<int>())).DomainEntity));

            fixture.Customize<MeasurementUnitCode>(n => n.FromFactory(() =>
                MeasurementUnitCode.Of((string)fixture.Create(
              new RegularExpressionRequest("^" + MeasurementUnitCode.Pattern + "$"),
              new SpecimenContext(fixture)))));
        }
    }
}

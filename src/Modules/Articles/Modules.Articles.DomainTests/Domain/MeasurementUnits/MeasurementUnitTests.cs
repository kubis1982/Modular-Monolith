namespace ModularMonolith.Modules.Articles.Domain.MeasurementUnits
{
    using System;

    public class MeasurementUnitTests : ModuleDomainTests
    {
        [Theory]
        [InlineDataFixture()]
        public void ShouldCreateMeasurementUnit(MeasurementUnit measurementUnit)
        {
            var @event = measurementUnit.Extensions().GetEvent<Events.MeasurementUnitCreatedEvent>();
            @event.MeasurementUnitId.Should().Be(measurementUnit.Id);
            @event.Code.Should().Be(measurementUnit.Code);
            @event.Name.Should().Be(measurementUnit.Name);
        }

        [Theory]
        [InlineDataFixture]
        public void ShouldNotCreateWhenCodeIsEmpty(string name, int measurementUnitId)
        {
            Action action = () => MeasurementUnit.Create("", name).Extensions().SetValue(n => n.Id, new MeasurementUnitId(measurementUnitId));
            action.Should().ThrowExactly<ArgumentException>();
        }

        [Theory]
        [InlineDataFixture]
        public void ShouldNotCreateWhenNameIsEmpty(MeasurementUnitCode code, int measurementUnitId)
        {
            Action action = () => MeasurementUnit.Create(code, "").Extensions().SetValue(n => n.Id, new MeasurementUnitId(measurementUnitId));
            action.Should().ThrowExactly<ArgumentException>();
        }

        [Theory]
        [InlineDataFixture]
        public void ShouldRemove(MeasurementUnit measurementUnit)
        {
            measurementUnit.Remove();
            var @event = measurementUnit.Extensions().GetEvent<Events.MeasurementUnitRemovedEvent>();
            @event.MeasurementUnitId.Should().Be(measurementUnit.Id);
        }

        [Theory]
        [InlineDataFixture]
        public void ShouldUpdate(MeasurementUnit measurementUnit, string name)
        {
            measurementUnit.Update(name);
            measurementUnit.Name.Should().Be(MeasurementUnitName.Of(name));
            var @event = measurementUnit.Extensions().GetEvent<Events.MeasurementUnitUpdatedEvent>();
            @event.MeasurementUnitId.Should().Be(measurementUnit.Id);
            @event.Name.Should().Be(measurementUnit.Name);

        }

        [Theory]
        [InlineDataFixture]
        public void ShouldNotChangeEmptyName(MeasurementUnit measurementUnit)
        {
            Action action = () => measurementUnit.Update("");
            action.Should().ThrowExactly<ArgumentException>();
        }
    }
}

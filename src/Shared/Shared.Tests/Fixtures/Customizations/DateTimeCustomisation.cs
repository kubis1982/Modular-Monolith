namespace ModularMonolith.Shared.Fixtures.Customizations
{
    using AutoFixture;
    using AutoFixture.Kernel;
    using System;

    internal class DateTimeCustomisation : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.Customizations.Add(new DateTimeGenerator());
        }
    }

    internal class DateTimeGenerator : ISpecimenBuilder
    {
        private readonly ISpecimenBuilder generator;

        public DateTimeGenerator()
        {
            this.generator = new RandomDateTimeSequenceGenerator();
        }

        public object Create(object request, ISpecimenContext context)
        {
            var t = request as Type;

            if (t == null || t != typeof(DateTime))
                return new NoSpecimen();

            var result = this.generator.Create(request, context);
            if (result is NoSpecimen)
                return result;

            return ((DateTime)result).ToUniversalTime();
        }
    }
}

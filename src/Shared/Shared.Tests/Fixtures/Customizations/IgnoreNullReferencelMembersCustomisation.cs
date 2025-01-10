namespace ModularMonolith.Shared.Fixtures.Customizations
{
    using AutoFixture;
    using AutoFixture.Kernel;
    using System;
    using System.Reflection;

    internal class IgnoreNullReferencelMembersCustomisation : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.Customizations.Add(new IgnoreNullReferencelMembers());
        }
    }

    internal class IgnoreNullReferencelMembers : ISpecimenBuilder
    {
        public object Create(object request, ISpecimenContext context)
        {
            ArgumentNullException.ThrowIfNull(context);

            var propertyInfo = request as PropertyInfo;
            if (propertyInfo == null || propertyInfo.PropertyType.IsValueType || propertyInfo.PropertyType == typeof(string))
            {
                return new NoSpecimen();
            }

            var nullabilityInfoContext = new NullabilityInfoContext();
            var nullability = nullabilityInfoContext.Create(propertyInfo);

            if (nullability.WriteState == NullabilityState.Nullable)
            {
                var instance = Activator.CreateInstance(propertyInfo.DeclaringType!);
                var currentValue = propertyInfo.GetValue(instance);

                if (currentValue is null)
                {
                    return new OmitSpecimen();
                }
            }
            return new NoSpecimen();
        }
    }
}

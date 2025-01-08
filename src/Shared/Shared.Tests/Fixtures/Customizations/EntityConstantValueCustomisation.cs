namespace ModularMonolith.Shared.Fixtures.Customizations {
    using AutoFixture;
    using AutoFixture.Kernel;
    using ModularMonolith.Shared.Persistance.WriteModel;
    using System;
    using System.Reflection;

    internal class EntityConstantValueCustomisation : ICustomization {
        public void Customize(IFixture fixture) {
            fixture.Customizations.Add(new EntityConstantValue());
        }
    }

    internal class EntityConstantValue : ISpecimenBuilder {
        public object Create(object request, ISpecimenContext context) {
            if (context == null) {
                throw new ArgumentNullException("context");
            }

            var pi = request as PropertyInfo;
            if (pi == null) {
                return new NoSpecimen();
            }

            if (pi.Name.StartsWith(ShadowProperties.CreatedBy)) {
                return 1; // Admin
            }

            if (pi.Name.StartsWith(ShadowProperties.CreatedOn)) {
                return DateTime.UtcNow; 
            }

            return new NoSpecimen();
        }
    }
}

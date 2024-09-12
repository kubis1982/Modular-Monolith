namespace Kubis1982.Shared.Customizations {
    using AutoFixture;
    using AutoFixture.Kernel;
    using System;
    using System.Reflection;

    public class IgnoreVirtualMembersCustomisation : ICustomization {
        public void Customize(IFixture fixture) {
            fixture.Customizations.Add(new IgnoreVirtualMembers());
        }
    }

    public class IgnoreVirtualMembers : ISpecimenBuilder {
        public object Create(object request, ISpecimenContext context) {
            if (context == null) {
                throw new ArgumentNullException("context");
            }

            var pi = request as PropertyInfo;
            if (pi == null) {
                return new NoSpecimen();
            }

            switch (pi.GetGetMethod()?.IsVirtual) {
                case true when pi.GetGetMethod()?.IsFinal == false:
                return null!;
                default:
                return new NoSpecimen();
            }
        }
    }
}

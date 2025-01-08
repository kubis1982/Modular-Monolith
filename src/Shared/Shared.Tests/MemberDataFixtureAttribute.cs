using AutoFixture;
using AutoFixture.Kernel;
using AutoFixture.Xunit2;
using ModularMonolith.Shared.Fixtures;
using System.Reflection;
using Xunit;
using Xunit.Sdk;

namespace ModularMonolith.Shared;

public class MemberDataFixtureAttribute(string memberName, params object[] parameters) : DataAttribute {
    private readonly Lazy<IFixture> fixture = new(FixtureFactory.Create, LazyThreadSafetyMode.PublicationOnly);
    private readonly MemberDataAttribute memberDataAttribute = new(memberName, parameters);

    public override IEnumerable<object[]> GetData(MethodInfo testMethod) {
        if (testMethod == null) {
            throw new ArgumentNullException(nameof(testMethod));
        } else {
            var memberData = memberDataAttribute.GetData(testMethod);

            using var enumerator = memberData.GetEnumerator();
            if (enumerator.MoveNext()) {
                var specimens = GetSpecimens(testMethod.GetParameters(), enumerator.Current.Length).ToArray();

                do {
                    yield return enumerator.Current.Concat(specimens).ToArray();
                } while (enumerator.MoveNext());
            }
        }
    }

    private IEnumerable<object> GetSpecimens(IEnumerable<ParameterInfo> parameters, int skip) {
        foreach (var parameter in parameters.Skip(skip)) {
            CustomizeFixture(parameter);
            yield return Resolve(parameter);
        }
    }

    private void CustomizeFixture(ParameterInfo p) {
        var customizeAttributes = p.GetCustomAttributes()
            .OfType<IParameterCustomizationSource>()
            .OrderBy(x => x, new CustomizeAttributeComparer());

        foreach (var ca in customizeAttributes) {
            var c = ca.GetCustomization(p);
            fixture.Value.Customize(c);
        }
    }

    private object Resolve(ParameterInfo p) {
        var context = new SpecimenContext(fixture.Value);
        return context.Resolve(p);
    }

    private class CustomizeAttributeComparer : Comparer<IParameterCustomizationSource> {
        public override int Compare(IParameterCustomizationSource? x, IParameterCustomizationSource? y) {
            var xfrozen = x is FrozenAttribute;
            var yfrozen = y is FrozenAttribute;

            if (xfrozen && !yfrozen) {
                return 1;
            }

            if (yfrozen && !xfrozen) {
                return -1;
            }

            return 0;
        }
    }
}

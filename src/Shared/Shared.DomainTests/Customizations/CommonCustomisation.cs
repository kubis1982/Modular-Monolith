namespace Kubis1982.Shared.Customizations
{
    using AutoFixture;
    using Kubis1982.Shared.Kernel.Types;

    internal class CommonCustomisation : ICustomization {
        public void Customize(IFixture fixture) {
            fixture.Customize<EntityTypeId>(x => x.FromFactory(() => EntityTypeId.Create(fixture.Create<string>().Substring(1,3), Convert.ToInt16(new Random().Next(1, 99)))));
        }
    }
}

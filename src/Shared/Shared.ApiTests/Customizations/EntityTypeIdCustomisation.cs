﻿namespace Kubis1982.Shared.Customizations
{
    using AutoFixture;
    using Kubis1982.Shared.Kernel.Types;

    internal class EntityTypeIdCustomisation : ICustomization {
        public void Customize(IFixture fixture) {
            fixture.Customize<EntityTypeId>(n => n.FromFactory(() => {
                byte number = fixture.Create<byte>();
                if (number > 99) {
                    number = 99;
                }
                string prefix = fixture.Create<string>().Substring(1, 3);
                return EntityTypeId.Create(fixture.Create<string>().Substring(1, 3), number);
            })); 
        }
    }
}

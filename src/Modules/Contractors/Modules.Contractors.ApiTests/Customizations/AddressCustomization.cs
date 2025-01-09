namespace ModularMonolith.Modules.Contractors.Customizations
{
    using AutoFixture;
    using ModularMonolith.Modules.Contractors.Domain;
    using ModularMonolith.Modules.Contractors.Persistance;
    using ModularMonolith.Modules.Contractors.Persistance.ReadModel.Entities;
    using ModularMonolith.Modules.Contractors.Requests;
    using ModularMonolith.Modules.Contractors.Requests.Common;

    internal class AddressCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.Customize<AddressEntity>(n => n.With(m => m.TypeId, EntityType.Address.Code.Value)
                .With(n => n.IsDefault, false)
                .With(m => m.Line1, GetSubstring(fixture.Create<string>(), ContractorRestriction.AddressLineLength))
                .With(m => m.Line2, GetSubstring(fixture.Create<string>(), ContractorRestriction.AddressLineLength))
                .With(m => m.PostalCode, GetSubstring(fixture.Create<string>(), ContractorRestriction.AddressPostalCodeLength))
                .With(m => m.City, GetSubstring(fixture.Create<string>(), ContractorRestriction.AddressCityLength))
                .With(m => m.Country, "PL"));

            fixture.Customize<CreateAddressRequest>(n => n
                .With(n => n.Country, "PL"));

            fixture.Customize<AddressRequest>(n => n
                .With(n => n.Country, "PL"));

            fixture.Customize<UpdateAddressRequest>(n => n
               .With(n => n.Country, "PL"));

            fixture.Customize<UpdateContractorAddressRequest>(n => n
              .With(n => n.Country, "PL"));
        }

        private string GetSubstring(string src, int length)
        {
            if (src.Length > length)
                return src.Substring(0, length);
            else
                return src;
        }
    }
}

namespace ModularMonolith.Modules.Contractors.Domain.Contractors
{
    using AutoFixture;
    using AutoFixture.Kernel;

    internal class ContractorCustomisation : ICustomization
    {
        public void Customize(IFixture fixture)
        {

            fixture.Customize<Address>(n => n.FromFactory(() =>
              Address.Create(fixture.Create<string>(), fixture.Create<string>(), fixture.Create<string>(), "Rawicz", "PL")));

            fixture.Customize<Country>(n => n.FromFactory(() => Country.Of("PL")));


            fixture.Customize<Contractor>(n => n.FromFactory(() =>
              Contractor.Create(fixture.Create<ContractorCode>(), fixture.Create<string>(), fixture.Create<string>(), fixture.Create<Country>(), fixture.Create<Address>())
                  .Extensions().SetValue(n => n.Id, new ContractorId(fixture.Create<int>())).DomainEntity));

            fixture.Customize<ContractorCode>(n => n.FromFactory(() =>
            ContractorCode.Of((string)fixture.Create(
                new RegularExpressionRequest("^" + ContractorCode.Pattern + "$"),
                new SpecimenContext(fixture)))));
        }
    }
}

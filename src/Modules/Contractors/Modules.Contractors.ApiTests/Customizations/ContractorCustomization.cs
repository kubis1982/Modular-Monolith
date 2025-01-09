namespace ModularMonolith.Modules.Contractors.Customizations
{
    using AutoFixture;
    using AutoFixture.Kernel;
    using ModularMonolith.Modules.Contractors.Domain;
    using ModularMonolith.Modules.Contractors.Persistance;
    using ModularMonolith.Modules.Contractors.Persistance.ReadModel.Entities;
    using ModularMonolith.Modules.Contractors.Requests;

    internal class Customization : ICustomization
    {

        public void Customize(IFixture fixture)
        {
            var code = (string)fixture.Create(
                    new RegularExpressionRequest(ContractorRestriction.CodePattern),
                    new SpecimenContext(fixture));

            fixture.Customize<ContractorEntity>(n => n.With(m => m.TypeId, EntityType.Contractor.Code.Value)
                .With(m => m.IsBlocked, false)
                .With(m => m.Country, "PL")
                .With(m => m.Code, code));

            fixture.Customize<CreateContractorRequest>(n => n
            .With(n => n.Country, "PL")
            .With(m => m.Code, (string)fixture.Create(
                new RegularExpressionRequest(ContractorRestriction.CodePattern),
                new SpecimenContext(fixture))));

            fixture.Customize<UpdateContractorRequest>(n => n
            .With(n => n.Country, "PL")
            .With(m => m.Code, (string)fixture.Create(
                new RegularExpressionRequest(ContractorRestriction.CodePattern),
                new SpecimenContext(fixture))));
        }
    }
}
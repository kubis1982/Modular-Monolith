namespace ModularMonolith.Modules.AccessManagement.Customizations
{
    using AutoFixture;
    using AutoFixture.Kernel;
    using ModularMonolith.Modules.AccessManagement.Endpoints.Requests.Users;
    using ModularMonolith.Modules.AccessManagement.Persistance;
    using ModularMonolith.Modules.AccessManagement.Persistance.ReadModel;
    using ModularMonolith.Shared;

    internal class UserCustomisation : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.Customize<UserEntity>(n => n
                .With(m => m.IsActive, true)
                .With(n => n.Email, () => $"{fixture.Create<string>().Substring(1, 15)}@{SystemInformation.DomainName}")
                .With(m => m.Id, () => fixture.Create(
                    new RangedNumberRequest(typeof(int), 10, int.MaxValue),
                    new SpecimenContext(fixture))));

            fixture.Customize<UpdateUserRequest>(n => n
                .With(n => n.FirstName, () => fixture.Create<string>().Substring(1, UserRestriction.FirstNameLength))
                .With(n => n.MiddleName, () => fixture.Create<string>().Substring(1, UserRestriction.MiddleNameLength))
                .With(n => n.LastName, () => fixture.Create<string>().Substring(1, UserRestriction.LastNameLength))
            );

            fixture.Customize<CreateUserRequest>(n => n
                .With(n => n.Email, () => $"{fixture.Create<string>().Substring(1, 15)}@{SystemInformation.DomainName}")
                .With(n => n.FirstName, () => fixture.Create<string>().Substring(1, UserRestriction.FirstNameLength))
                .With(n => n.MiddleName, () => fixture.Create<string>().Substring(1, UserRestriction.MiddleNameLength))
                .With(n => n.LastName, () => fixture.Create<string>().Substring(1, UserRestriction.LastNameLength))
            );
        }
    }
}

namespace ModularMonolith.Modules.AccessManagement.Customizations
{
    using AutoFixture;
    using AutoFixture.Kernel;
    using ModularMonolith.Modules.AccessManagement.Domain.Users;
    using ModularMonolith.Shared.Extensions;

    internal class UserCustomisation : ICustomization {
        public void Customize(IFixture fixture) {
            fixture.Customize<UserEmail>(n => n.FromFactory(() => (UserEmail)SystemInformation.Email));

            fixture.Customize<User>(n => n.FromFactory(() => User.Create(fixture.Create<UserEmail>(), fixture.Create<UserPassword>(), fixture.Create<UserFullName>())
                .Extensions().SetValue(n => n.Id, new UserId((int)fixture.Create(
                    new RangedNumberRequest(typeof(int), 5, int.MaxValue),
                    new SpecimenContext(fixture)))).DomainEntity));            
        }
    }
}

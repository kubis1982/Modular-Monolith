namespace Kubis1982.Modules.AccessManagement.Customizations
{
    using AutoFixture;
    using AutoFixture.Kernel;
    using Kubis1982.Modules.AccessManagement.Domain.Users;
    using Kubis1982.Shared.Extensions;

    internal class UserCustomisation : ICustomization {
        public void Customize(IFixture fixture) {
            fixture.Customize<UserEmail>(n => n.FromFactory(() => (UserEmail)$"user@kubis1982.com"));
            fixture.Customize<UserFullName>(n => n.FromFactory(() => UserFullName.Create(fixture.Create<string>(), fixture.Create<string>(), fixture.Create<string>())));
            fixture.Customize<UserPassword>(n => n.FromFactory(() => (UserPassword)fixture.Create<string>()));

            fixture.Customize<User>(n => n.FromFactory(() => User.Create(fixture.Create<UserEmail>(), fixture.Create<UserPassword>(), fixture.Create<UserFullName>())
                .Extensions().SetValue(n => n.Id, new UserId((int)fixture.Create(
                    new RangedNumberRequest(typeof(int), 5, int.MaxValue),
                    new SpecimenContext(fixture)))).DomainEntity));

            
        }
    }
}

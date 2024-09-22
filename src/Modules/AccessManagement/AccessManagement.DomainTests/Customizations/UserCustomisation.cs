namespace ModularMonolith.Modules.AccessManagement.Customizations
{
    using AutoFixture;
    using AutoFixture.Kernel;
    using ModularMonolith.Modules.AccessManagement.Domain.Users;
    using ModularMonolith.Shared.Extensions;

    internal class UserCustomisation : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.Customize<UserEmail>(n => n.FromFactory(() => (UserEmail)SystemInformation.Email));
            fixture.Customize<UserFullName>(n => n.FromFactory(() => UserFullName.Create(fixture.Create<string>(), fixture.Create<string>(), fixture.Create<string>())));
            fixture.Customize<UserPassword>(n => n.FromFactory(() => (UserPassword)fixture.Create<string>()));
            fixture.Customize<UserToken>(n => n.FromFactory(() => UserToken.Create(DateTime.Now.AddHours(1))));

            fixture.Customize<User>(n => n.FromFactory(() => User.Create(fixture.Create<UserEmail>(), fixture.Create<UserPassword>(), fixture.Create<UserFullName>())
                .Extensions().SetValue(n => n.IsActive, true).SetValue(n => n.Id, new UserId((int)fixture.Create(
                    new RangedNumberRequest(typeof(int), 5, int.MaxValue),
                    new SpecimenContext(fixture)))).DomainEntity));

            fixture.Customize<RefreshToken>(n => n.FromFactory(() => RefreshToken.Create(fixture.Create<string>(), fixture.Create<DateTime>())));
            fixture.Customize<Session>(n => n.FromFactory(() => Session.Create(fixture.Create<DateTime>(), fixture.Create<RefreshToken>())
                .Extensions().SetValue(n => n.Id, new SessionId((int)fixture.Create<int>())).DomainEntity));
        }
    }
}

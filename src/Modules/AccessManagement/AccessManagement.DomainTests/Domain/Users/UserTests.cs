namespace Kubis1982.Modules.AccessManagement.Domain.Users
{
    using Kubis1982.Modules.AccessManagement.Domain.Users.Events;
    using Kubis1982.Shared;
    using Kubis1982.Shared.Extensions;
    using Kubis1982.Shared.Time;
    using System;

    public class UserTests : AbstractTests {
        [Theory]
        [AutoFixture]
        public void ShouldCreateUser(UserEmail userEmail, UserPassword userPassword, UserFullName userFullName) {
            // Act
            User user = User.Create(userEmail, userPassword, userFullName);

            // Assert
            var @event = user.Extensions().GetEvent<Events.UserCreatedEvent>();
            @event.UserId.Should().Be(user.Id);
            user.Password.Should().Be(user.Password);
        }

        [Theory]
        [AutoFixture]
        public void ShouldChangePassword(User user, UserPassword newPassword) {
            // Act
            user.ChangePassword(newPassword);

            // Assert
            var @event = user.Extensions().GetEvent<UserPasswordChangedEvent>();
            @event.UserId.Should().Be(user.Id);
            user.Password.Should().Be(newPassword);
        }

        [Theory]
        [AutoFixture]
        public void ShouldUpdateUser(User user, UserFullName userFullName) {
            // Act
            user.Update(userFullName);

            // Assert
            var @event = user.Extensions().GetEvent<Events.UserUpdatedEvent>();
            @event.UserFullName.Should().Be(userFullName);
        }

        [Theory]
        [AutoFixture]
        public void ShouldDeactivateUser(User user, User currentUser) {
            // Arrange
            user.Extensions().SetValue(n => n.IsActive, true);

            // Act
            user.Deactivate(currentUser);

            // Assert
            var @event = user.Extensions().GetEvent<UserDeactivatedEvent>();
            @event.UserId.Should().Be(user.Id);
            user.IsActive.Should().BeFalse();
        }

        [Theory]
        [AutoFixture]
        public void ShouldNotDeactivateHimself(User user) {
            // Act
            Action action = () => user.Deactivate(user);

            // Assert
            action.Should().Throw<ActionOnCurrentUserException>();
        }

        [Theory]
        [AutoFixture]
        public void ShouldActivateUser(User user, User currentUser) {
            // Arrange
            user.Extensions().SetValue(n => n.IsActive, false);

            // Act
            user.Activate(currentUser);

            // Assert
            var @event = user.Extensions().GetEvent<UserActivatedEvent>();
            @event.UserId.Should().Be(user.Id);
            user.IsActive.Should().BeTrue();
        }

        [Theory]
        [AutoFixture]
        public void ShouldDeleteUser(User user, User currentUser) {
            // Act
            user.Delete(currentUser);

            // Assert
            var @event = user.Extensions().GetEvent<UserDeletedEvent>();
            @event.UserId.Should().Be(user.Id);
            @event.CurrentUserId.Should().Be(currentUser.Id);
        }

        [Theory]
        [AutoFixture]
        public void ShouldCreateSession(User user, DateTime dateTime, UserPassword userPassword)
        {
            // Arrange
            user.Extensions().SetValue(n => n.Password, userPassword);
            var clock = Mock.Of<IClock>(n => n.Now == dateTime);
            RefreshToken refreshToken = RefreshToken.Of("Próba", dateTime);

            // Act
            user.CreateSession(userPassword, dateTime, refreshToken);

            // Assert
            var @event = user.Extensions().GetEvent<SessionCreatedEvent>();
            @event.ExpiryDate.Should().Be(dateTime);
            @event.UserId.Should().Be(user.Id);
        }


        [Theory]
        [AutoFixture]
        public void ShouldRefreshSession(User user, SessionId sessionId, string refreshToken, string newRefreshToken)
        {
            // Arrange
            DateTime dateTime = DateTime.UtcNow;
            var clock = Mock.Of<IClock>(n => n.Now == dateTime);
            RefreshToken token = RefreshToken.Of(refreshToken, dateTime);
            RefreshToken newToken = RefreshToken.Of(newRefreshToken, dateTime);
            Session session = Session.Create(dateTime, token).Extensions().SetValue(n => n.Id, sessionId).DomainEntity;
            user.Extensions().SetList(n => n.Sessions, [session]);

            // Act
            user.RefreshSession(sessionId, refreshToken, dateTime, newToken, clock);

            // Assert
            var @event = user.Extensions().GetEvent<SessionExpiryDateExtendedEvent>();
            @event.ExpiryDate.Should().Be(dateTime);
        }
    }
}

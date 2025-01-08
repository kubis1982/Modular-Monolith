namespace ModularMonolith.Modules.AccessManagement.Domain.Users
{
    using ModularMonolith.Modules.AccessManagement.Domain.Users.Events;
    using ModularMonolith.Shared;
    using ModularMonolith.Shared.Extensions;
    using ModularMonolith.Shared.Time;
    using System;

    public class UserTests : DomainTests
    {
        [Theory]
        [InlineDataFixture]
        public void ShouldCreateUser(UserEmail userEmail, UserPassword userPassword, UserFullName userFullName)
        {
            // Act
            User user = User.Create(userEmail, userPassword, userFullName);

            // Assert
            var @event = user.Extensions().GetEvent<Events.UserCreatedEvent>();
            @event.UserId.Should().Be(user.Id);
            user.Password.Should().Be(user.Password);
        }

        [Theory]
        [InlineDataFixture]
        public void ShouldChangePassword(User user, UserPassword newPassword)
        {
            // Act
            user.ChangePassword(newPassword);

            // Assert
            var @event = user.Extensions().GetEvent<UserPasswordChangedEvent>();
            @event.UserId.Should().Be(user.Id);
            user.Password.Should().Be(newPassword);
        }

        [Theory]
        [InlineDataFixture]
        public void ShouldUpdateUser(User user, UserFullName userFullName)
        {
            // Act
            user.Update(userFullName);

            // Assert
            var @event = user.Extensions().GetEvent<Events.UserUpdatedEvent>();
            @event.UserFullName.Should().Be(userFullName);
        }

        [Theory]
        [InlineDataFixture]
        public void ShouldDeactivateUser(User user, User currentUser)
        {
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
        [InlineDataFixture]
        public void ShouldNotDeactivateHimself(User user)
        {
            // Act
            Action action = () => user.Deactivate(user);

            // Assert
            action.Should().Throw<ActionOnCurrentUserException>();
        }

        [Theory]
        [InlineDataFixture]
        public void ShouldActivateUser(User user, User currentUser)
        {
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
        [InlineDataFixture]
        public void ShouldDeleteUser(User user, User currentUser)
        {
            // Act
            user.Delete(currentUser);

            // Assert
            var @event = user.Extensions().GetEvent<UserDeletedEvent>();
            @event.UserId.Should().Be(user.Id);
            @event.CurrentUserId.Should().Be(currentUser.Id);
        }

        [Theory]
        [InlineDataFixture]
        public void ShouldNotDeleteUserIfUserHasSessions(User user, User currentUser, Session session)
        {
            // Arrange
            user.Extensions().SetValue(n => n.Sessions, [session]);

            // Act
            var action = () => user.Delete(currentUser);

            // Assert
            action.Should().ThrowExactly<UserHasSessionsException>();
        }

        [Theory]
        [InlineDataFixture]
        public void ShouldCreateSession(User user, DateTime dateTime, UserPassword userPassword)
        {
            // Arrange
            user.Extensions().SetValue(n => n.Password, userPassword);
            var clock = Mock.Of<IClock>(n => n.Now == dateTime);
            RefreshToken refreshToken = RefreshToken.Create("Próba", dateTime);

            // Act
            user.CreateSession(userPassword, dateTime, refreshToken);

            // Assert
            var @event = user.Extensions().GetEvent<SessionCreatedEvent>();
            @event.ExpiryDate.Should().Be(dateTime);
            @event.UserId.Should().Be(user.Id);
        }


        [Theory]
        [InlineDataFixture]
        public void ShouldRefreshSession(User user, SessionId sessionId, string refreshToken, string newRefreshToken)
        {
            // Arrange
            DateTime dateTime = DateTime.UtcNow;
            var clock = Mock.Of<IClock>(n => n.Now == dateTime);
            RefreshToken token = RefreshToken.Create(refreshToken, dateTime);
            RefreshToken newToken = RefreshToken.Create(newRefreshToken, dateTime);
            Session session = Session.Create(dateTime, token).Extensions().SetValue(n => n.Id, sessionId).DomainEntity;
            user.Extensions().SetValue(n => n.Sessions, [session]);

            // Act
            user.RefreshSession(sessionId, refreshToken, dateTime, newToken, clock);

            // Assert
            var @event = user.Extensions().GetEvent<SessionExpirationDateExtendedEvent>();
            @event.ExpirationDate.Should().Be(dateTime);
        }

        [Theory]
        [InlineDataFixture]
        public void ShouldCreateToken(User user)
        {
            // Arrange
            Mock<IClock> clock = new();

            // Act
            user.CreateToken(clock.Object);

            // Assert
            var @event = user.Extensions().GetEvent<UserTokenCreatedEvent>();
            @event.UserId.Should().Be(user.Id);
        }

        [Theory]
        [InlineDataFixture]
        public void ShouldNotCreateTokenIfUserIsDeactivated(User user)
        {
            // Arrange
            user.Extensions().SetValue(n => n.IsActive, false);

            // Act
            Action action = () => user.CreateToken(Mock.Of<IClock>());

            // Assert
            action.Should().ThrowExactly<UserIsUnactiveException>();
        }

        [Theory]
        [InlineDataFixture]
        public void ShouldFinishToken(User user, UserToken userCode, string password)
        {
            // Arrange
            Mock<IClock> clock = new();
            clock.Setup(n => n.Now).Returns(DateTime.UtcNow);

            user.Extensions().SetValue(n => n.Token, userCode);

            // Act
            user.FinishToken(userCode.Token, (UserPassword)password, clock.Object);

            // Assert
            var @event = user.Extensions().GetEvent<UserTokenFinishedEvent>();
            @event.UserId.Should().Be(user.Id);
            user.Password.Should().Be((UserPassword)password);
        }

        [Theory]
        [InlineDataFixture]
        public void ShouldNotFinishTokenIfTokenIsExpirated(User user, UserToken userCode)
        {
            // Arrange
            Mock<IClock> clock = new Mock<IClock>();
            clock.Setup(n => n.Now).Returns(userCode.ExpirationDate.AddHours(1));

            // Act
            var action = () => user.FinishToken(userCode.Token, (UserPassword)"Password", clock.Object);

            // Assert
            action.Should().ThrowExactly<InvalidUserTokenException>();
        }

        [Theory]
        [InlineDataFixture]
        public void ShouldNotFinishTokenIfTokenIsNotValid(User user, UserToken userCode, Guid token)
        {
            // Arrange
            Mock<IClock> clock = new();
            clock.Setup(n => n.Now).Returns(DateTime.UtcNow);

            user.Extensions().SetValue(n => n.Token, userCode);

            // Act
            var action = () => user.FinishToken(token, (UserPassword)"Password", clock.Object);

            // Assert
            action.Should().ThrowExactly<InvalidUserTokenException>();
        }
    }
}

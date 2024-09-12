namespace Kubis1982.Modules.AccessManagement.Domain.Users
{
    using Kubis1982.Modules.AccessManagement.Domain.Users.Exceptions;
    using Kubis1982.Shared;
    using Kubis1982.Shared.Extensions;
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
            var @event = user.Extensions().GetEvent<Events.UserPasswordChangedEvent>();
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
            var @event = user.Extensions().GetEvent<Events.UserDeactivatedEvent>();
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
            var @event = user.Extensions().GetEvent<Events.UserActivatedEvent>();
            @event.UserId.Should().Be(user.Id);
            user.IsActive.Should().BeTrue();
        }

        [Theory]
        [AutoFixture]
        public void ShouldDeleteUser(User user, User currentUser) {
            // Act
            user.Delete(currentUser);

            // Assert
            var @event = user.Extensions().GetEvent<Events.UserDeletedEvent>();
            @event.UserId.Should().Be(user.Id);
            @event.CurrentUserId.Should().Be(currentUser.Id);
        }
    }
}

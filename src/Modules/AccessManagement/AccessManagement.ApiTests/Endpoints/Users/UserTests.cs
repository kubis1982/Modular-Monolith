using FluentAssertions.Extensions;
using Microsoft.Extensions.DependencyInjection;
using ModularMonolith.Modules.AccessManagement.CQRS.Queries.Users;
using ModularMonolith.Modules.AccessManagement.Domain.Users;
using ModularMonolith.Modules.AccessManagement.Endpoints.Requests.Users;
using ModularMonolith.Modules.AccessManagement.Persistance.ReadModel;
using ModularMonolith.Shared;
using ModularMonolith.Shared.Persistance.Exceptions;
using ModularMonolith.Shared.Time;
using Moq;
using Xunit.Abstractions;

namespace ModularMonolith.Modules.AccessManagement.Endpoints.Users
{

    public class UserTests(WebApplicationFixture webApplicationFixture, ITestOutputHelper testOutputHelper) : ModuleApiTests(webApplicationFixture, testOutputHelper)
    {
        [Theory]
        [InlineDataFixture($"temp@{SystemInformation.DomainName}")]
        public async Task ShouldCreateNewUser(string email, CreateUserRequest request)
        {
            // Arrange
            request.Email = email;

            // Act
            var identity = await HttpClient.PostAndReturnIdentityAsync($"/users", request);

            // Assert
            var user = DbContext.Users.Single(n => n.Id == identity.Id);
            user.FirstName.Should().Be(request.FirstName);
            user.MiddleName.Should().Be(request.MiddleName);
            user.LastName.Should().Be(request.LastName);
            user.IsActive.Should().BeTrue();
            user.Email.Should().Be(email);
        }

        [Theory]
        [InlineDataFixture]
        public async Task ShouldNotCreateNewUserIfEmailAlreadyExists(CreateUserRequest request)
        {
            // Arrange
            request.Email = User.Administrator.Email;

            // Act
            var action = async () => await HttpClient.PostAndReturnIdentityAsync($"/users", request);

            // Assert
            await action.Should().ThrowExactlyAsync<HttpRequestException>().Where(n =>
                n.Message.Contains(new UniqueConstraintException(null).Message));
        }

        [Theory]
        [InlineDataFixture]
        public async Task ShouldUpdateUser(UserEntity user, UpdateUserRequest request)
        {
            // Arrange
            DbContext.Add(user);
            DbContext.SaveChanges();

            // Act
            await HttpClient.PutAsync($"/users/{user.Id}", request);

            // Assert
            var result = DbContext.Users.Single(n => n.Id == user.Id);
            result.FirstName.Should().Be(request.FirstName);
            result.MiddleName.Should().Be(request.MiddleName);
            result.LastName.Should().Be(request.LastName);
            result.IsActive.Should().Be(user.IsActive);
            result.Email.Should().Be(user.Email);
        }

        [Theory]
        [InlineDataFixture]
        public async Task ShouldChangePasswordUser(UserEntity user, ChangePasswordRequest request)
        {
            // Arrange
            DbContext.Add(user);
            DbContext.SaveChanges();

            // Act
            await HttpClient.PatchAsync($"/users/{user.Id}/change-password", request);

            // Assert
            var result = DbContext.Users.Single(n => n.Id == user.Id);
            result.Password.Should().Be(UserPassword.Create(request.Password, Services.GetRequiredService<IPasswordHasher>()).Hash);
        }

        [Theory]
        [InlineDataFixture]
        public async Task ShouldDeactivateUser(UserEntity user)
        {
            // Arrange
            DbContext.Add(user);
            DbContext.SaveChanges();

            // Act
            await HttpClient.PatchAsync($"/users/{user.Id}/deactivate");

            // Assert
            DbContext.Users.Any(n => n.Id == user.Id && n.IsActive == false).Should().BeTrue();
        }

        [Theory()]
        [InlineDataFixture]
        public async Task ShouldActivateUser(UserEntity user)
        {
            // Arrange
            user.IsActive = false;
            DbContext.Add(user);
            DbContext.SaveChanges();

            // Act
            await HttpClient.PatchAsync($"/users/{user.Id}/activate");

            // Assert
            DbContext.Users.Any(n => n.Id == user.Id && n.IsActive == true).Should().BeTrue();
        }

        #region Delete User

        [Theory]
        [InlineDataFixture]
        public async Task ShouldDeleteUser(UserEntity user)
        {
            // Arrange
            DbContext.Add(user);
            DbContext.SaveChanges();

            // Act
            await HttpClient.DeleteAndEnsureNoContentAsync($"/users/{user.Id}");

            // Assert
            DbContext.Users.SingleOrDefault(n => n.Id == user.Id).Should().BeNull();
        }

        #endregion

        [Fact]
        public async Task ShouldGetUser()
        {
            // Act
            var result = await HttpClient.GetAsync<GetUserQueryResult>($"/users/{User.Administrator.Id.Id}");

            // Assert
            result.Email.Should().Be(User.Administrator.Email!.Value);
            result.FirstName.Should().Be(User.Administrator.FullName!.FirstName!);
            result.Id.Should().Be(User.Administrator.Id.Id);
            result.TypeId.Should().Be(User.Administrator.Id.TypeId);
            result.IsActive.Should().Be(User.Administrator.IsActive);
            result.LastName.Should().Be(User.Administrator.FullName.LastName!);
            result.MiddleName.Should().Be(User.Administrator.FullName.MiddleName!);
        }

        [Theory]
        [InlineDataFixture]
        public async Task ShouldCreateToken(UserEntity userEntity, CreateUserCodeRequest request)
        {
            // Arrange
            request.Email = userEntity.Email;

            DbContext.Add(userEntity);
            DbContext.SaveChanges();

            // Act
            await HttpClient.PostAsync($"/reset-password", request);

            // Assert
            var user = DbContext.Users.Single(n => n.Id == userEntity.Id);
            user.Token.Should().NotBeNull();
            user.TokenExpirationDate.Should().NotBeNull();
            user.Token.Should().NotBe(Guid.Empty);
        }

        [Theory]
        [InlineDataFixture]
        public async Task ShouldFinishToken(UserEntity userEntity, UserToken userToken, ChangePasswordByTokenRequest request)
        {
            // Arrange
            Mock<IClock> clockMock = new Mock<IClock>();
            clockMock.Setup(n => n.Now).Returns(DateTime.UtcNow);

            ChangeServices(n =>
            {
                n.AddScoped(n => clockMock.Object);
            });

            request.Token = userToken.Token;

            userEntity.Token = userToken.Token;
            userEntity.TokenExpirationDate = DateTime.Now.AddDays(1).AsUtc();

            DbContext.Add(userEntity);
            DbContext.SaveChanges();

            // Act
            await HttpClient.PatchAsync($"/confirm-password", request);

            // Assert
            var user = DbContext.Users.Single(n => n.Id == userEntity.Id);
            IPasswordHasher passwordHasher = Services.GetRequiredService<IPasswordHasher>();
            user.Token.Should().BeNull();
            user.TokenExpirationDate.Should().BeNull();
            user.Password.Should().Be(UserPassword.Create(request.Password, passwordHasher).Hash);
        }
    }
}

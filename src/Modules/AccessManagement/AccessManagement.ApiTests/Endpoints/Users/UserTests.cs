using ModularMonolith.Modules.AccessManagement.Endpoints.Requests.Users;
using ModularMonolith.Modules.AccessManagement.Persistance.ReadModel;
using ModularMonolith.Shared;
using Xunit.Abstractions;

namespace ModularMonolith.Modules.AccessManagement.Endpoints.Users
{

    public class UserTests(ITestOutputHelper testOutputHelper) : ApiTests(testOutputHelper)
    {
        [Theory]
        [AutoFixture($"temp@{SystemInformation.DomainName}")]
        public async Task ShouldCreateNewUser(string email, CreateUserRequest request)
        {
            // Arrange
            request.Email = email;

            // Act
            var identity = await TestHelper.HttpClient.PostAndReturnIdentityAsync($"/users", request);

            // Assert
            var user = TestHelper.Db.GetSingle<UserEntity>(n => n.Id == identity.Id);
            user.FirstName.Should().Be(request.FirstName);
            user.MiddleName.Should().Be(request.MiddleName);
            user.LastName.Should().Be(request.LastName);
            user.IsActive.Should().BeTrue();
            user.Email.Should().Be(email == "" ? null : email);
        }

        //    [Theory]
        //    [AutoFixture]
        //    public async Task ShouldNotCreateNewUserWhenEmailAlreadyExists(UserEntity[] users, CreateUserRequest request) {
        //        // Arrange
        //        TestHelper.Db.Add(users);

        //        request.Email = users.Last().Email;

        //        // Act
        //        var action = async () => await TestHelper.HttpClient.PostAndReturnIdentityAsync($"/users", request);

        //        // Assert
        //        await action.Should().ThrowExactlyAsync<HttpRequestException>(new UniqueConstraintException(null).Message);
        //    }

        //    [Theory()]
        //    [AutoFixture("admin@7technology.pl")]
        //    [AutoFixture("")]
        //    public async Task ShouldUpdateUser(string email, UserEntity user, UpdateUserRequest request) {
        //        // Arrange
        //        TestHelper.Db.Add(user);

        //        request.Email = email;

        //        // Act
        //        await TestHelper.HttpClient.PutAsync($"/users/{user.Id}", request);

        //        // Assert
        //        var result = TestHelper.Db.GetSingle<UserEntity>(n => n.Id == user.Id);
        //        result.Name.Should().Be(request.Name);
        //        result.FirstName.Should().Be(request.FirstName);
        //        result.MiddleName.Should().Be(request.MiddleName);
        //        result.LastName.Should().Be(request.LastName);
        //        result.IsBlocked.Should().BeFalse();
        //        result.Email.Should().Be(string.IsNullOrWhiteSpace(email) ? null : email);
        //    }

        //    [Theory]
        //    [AutoFixture]
        //    public async Task ShouldChangePasswordUser(UserEntity user, ChangePasswordRequest request) {
        //        // Arrange
        //        TestHelper.Db.Add(user);

        //        // Act
        //        await TestHelper.HttpClient.PatchAsync($"/users/{user.Id}/change-password", request);

        //        // Assert
        //        var result = TestHelper.Db.GetSingle<UserEntity>(n => n.Id == user.Id);
        //        result.Password.Should().Be(UserPassword.Create(request.Password, TestHelper.Services.GetRequiredService<IPasswordHasher>()));
        //        result.RequirePasswordReset.Should().BeTrue();
        //    }

        //    [Theory]
        //    [AutoFixture]
        //    public async Task ShouldUnblockUser(UserEntity user) {
        //        // Arrange
        //        user.IsBlocked = true;
        //        TestHelper.Db.Add(user);

        //        // Act
        //        await TestHelper.HttpClient.PatchAsync($"/users/{user.Id}/unblock");

        //        // Assert
        //        var result = TestHelper.Db.GetSingle<UserEntity>(n => n.Id == user.Id).IsBlocked.Should().BeFalse();
        //    }

        //    [Theory()]
        //    [AutoFixture]
        //    public async Task ShouldBlockUser(UserEntity user) {
        //        // Arrange
        //        TestHelper.Db.Add(user);

        //        // Act
        //        await TestHelper.HttpClient.PatchAsync($"/users/{user.Id}/block");

        //        // Assert
        //        var result = TestHelper.Db.GetSingle<UserEntity>(n => n.Id == user.Id).IsBlocked.Should().BeTrue();
        //    }

        //    #region Delete User

        //    [Theory]
        //    [AutoFixture]
        //    public async Task ShouldDeleteUser(UserEntity user) {
        //        // Arrange
        //        TestHelper.Db.Add(user);

        //        // Act
        //        await TestHelper.HttpClient.DeleteAndEnsureNoContentAsync($"/users/{user.Id}");

        //        // Assert
        //        TestHelper.Db.GetSingleOrDefault<UserEntity>(n => n.Id == user.Id).Should().BeNull();
        //    }

        //    [Theory]
        //    [AutoFixture]
        //    public async Task ShouldNotDeleteUserIfUserHasSessions(UserEntity user, SessionEntity sessionEntity) {
        //        // Arrange
        //        TestHelper.Db.Add(user);

        //        sessionEntity.CreatedBy = user.Id;

        //        TestHelper.Db.Add(sessionEntity);

        //        // Act
        //        var func = async () => await TestHelper.HttpClient.DeleteAndEnsureNoContentAsync($"/users/{user.Id}");

        //        // Assert
        //        await func.Should().ThrowExactlyAsync<HttpRequestException>().Where(n => n.Message.Contains(new UserHasSessionsException().Message));
        //    }

        //    #endregion

        //    [Fact]
        //    public async Task ShouldGetUser() {
        //        // Act
        //        var result = await TestHelper.HttpClient.GetAsync<GetUsersQueryResult>($"/users/1");

        //        // Assert
        //        result.Should().NotBeNull();
        //        result.Email.Should().Be(User.Administrator.Email!.Value);
        //        result.FirstName.Should().Be(User.Administrator.FullName!.FirstName!);
        //        result.Id.Should().Be(User.Administrator.Id.Value);
        //        result.TypeId.Should().Be(User.Administrator.Id.TypeId);
        //        result.IsBlocked.Should().Be(User.Administrator.IsBlocked);
        //        result.LastName.Should().Be(User.Administrator.FullName.LastName!);
        //        result.MiddleName.Should().Be(User.Administrator.FullName.MiddleName!);
        //        result.Name.Should().Be(User.Administrator.Name);
        //        result.Roles.Should().BeEquivalentTo(new[] { RoleId.User.Value, RoleId.Administrator.Value });
        //        result.Role.Should().NotBeNull();
        //        result.Role.Code.Should().Be(Role.AdministratorRole.Code);
        //        result.Role.Id.Should().Be(RoleId.Administrator.Value);
        //        result.Role.Level.Should().Be(2);
        //        result.Role.Name.Should().Be(Role.AdministratorRole.Name);
        //        result.Role.ParentId.Should().Be(Role.UserRole.Id);
        //        result.Role.Path.Should().Be(Role.AdministratorRole.Path);
        //    }
    }
}

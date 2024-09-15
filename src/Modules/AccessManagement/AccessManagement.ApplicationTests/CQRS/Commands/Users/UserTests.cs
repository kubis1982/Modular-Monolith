namespace ModularMonolith.Modules.AccessManagement.CQRS.Commands.Users
{
    using ModularMonolith.Modules.AccessManagement.Domain.Users;
    using ModularMonolith.Shared;
    using System.Threading.Tasks;

    public class UserTests : ApplicationTests {
        [Theory]
        [AutoFixture]
        public async Task ShouldCreateUserAsync(CreateUserCommand createUserCommand, User user) {
            // Arrange
            var passwordHasher = Mock.Of<IPasswordHasher>(n => n.Compute(It.Is<string>(m => m.Equals(createUserCommand.Password))) == createUserCommand.Password + "New");
            var userRepository = Mock.Of<IUserRepository>(n => n.AddAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()) == Task.FromResult(user));
            var handler = new CreateUserCommand.CreateUserCommandHandler(userRepository, passwordHasher);
            createUserCommand = createUserCommand with {  Email = SystemInformation.Email };            

            // Act
            var result = await handler.Handle(createUserCommand, default);

            // Assert
            result.TypeId.Should().Be(user.Id.TypeId);
            result.Id.Should().Be(user.Id.Id);            
        }
    }
}

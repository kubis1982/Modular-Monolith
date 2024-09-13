namespace Kubis1982.Modules.AccessManagement.Persistance.WriteModel.Repositories
{
    using Kubis1982.Modules.AccessManagement.Exceptions.Users;
    using Kubis1982.Shared.Exceptions;

    internal class UserRepository(WriteDbContext dbContext) : Repository<User, UserSpec>(dbContext), IUserRepository
    {
        protected override EntityNotFoundException CreateEntityNotFoundException()
        {
            return new UserNotFoundException();
        }
    }
}

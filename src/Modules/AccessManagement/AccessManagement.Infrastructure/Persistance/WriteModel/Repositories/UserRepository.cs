namespace ModularMonolith.Modules.AccessManagement.Persistance.WriteModel.Repositories
{
    using ModularMonolith.Modules.AccessManagement.Exceptions.Users;
    using ModularMonolith.Shared.Exceptions;

    internal class UserRepository(WriteDbContext dbContext) : Repository<User, UserSpec>(dbContext), IUserRepository
    {
        protected override EntityNotFoundException CreateEntityNotFoundException()
        {
            return new UserNotFoundException();
        }
    }
}

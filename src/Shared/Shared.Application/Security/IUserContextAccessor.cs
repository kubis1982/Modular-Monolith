namespace ModularMonolith.Shared.Security
{
    public interface IUserContextAccessor
    {
        IUserContext Get();
    }
}

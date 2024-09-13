namespace Kubis1982.Shared.Persistance
{
    using System.Threading;
    using System.Threading.Tasks;

    public interface IUnitOfWork
    {
        Task<int> Commit(CancellationToken cancellationToken = default);
    }
}

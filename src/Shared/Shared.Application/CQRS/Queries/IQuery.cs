namespace Kubis1982.Shared.CQRS.Queries
{
    using MediatR;

    public interface IQuery<TResponse> : IRequest<TResponse>
    {
    }
}

namespace ModularMonolith.Shared.CQRS.Queries
{
    using MediatR;

    public interface IQuery<TResponse> : IRequest<TResponse>
    {
    }
}

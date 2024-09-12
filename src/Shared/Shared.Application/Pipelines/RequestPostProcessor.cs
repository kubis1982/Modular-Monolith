namespace Kubis1982.Shared.Pipelines
{
    using MediatR;
    using MediatR.Pipeline;
    using System.Threading;
    using System.Threading.Tasks;

    public abstract class RequestPostProcessor<TRequest, TResponse> : IRequestPostProcessor<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        public abstract Task Process(TRequest request, TResponse response, CancellationToken cancellationToken);
    }
}

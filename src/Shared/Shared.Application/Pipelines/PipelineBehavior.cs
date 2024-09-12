namespace Kubis1982.Shared.Pipelines
{
    using MediatR;
    using System.Threading;
    using System.Threading.Tasks;

    public abstract class PipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IBaseRequest
    {
        public abstract Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken);
    }
}

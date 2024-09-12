namespace Kubis1982.Shared.Pipelines
{
    using Kubis1982.Shared.CQRS.Commands;
    using Kubis1982.Shared.Extensions;
    using Kubis1982.Shared.Persistance;
    using MediatR;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    internal class UnitOfWorkPipelineBehavior<TRequest, TResponse>(IServiceProvider serviceProvider) : PipelineBehavior<TRequest, TResponse> where TRequest : IUnitOfWorkCommand
    {
        public override async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken = default)
        {
            var response = await next();
            var unitOfWork = serviceProvider.GetRequiredKeyedService<IUnitOfWork>(request.GetModuleName());
            await unitOfWork.CommitAsync(cancellationToken);
            return response;
        }
    }
}

namespace ModularMonolith.Shared.Pipelines
{
    using MediatR;
    using Microsoft.Extensions.DependencyInjection;
    using ModularMonolith.Shared.CQRS.Commands;
    using ModularMonolith.Shared.Extensions;
    using ModularMonolith.Shared.Persistance;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    internal class UnitOfWorkPipelineBehavior<TRequest, TResponse>(IServiceProvider serviceProvider) : PipelineBehavior<TRequest, TResponse> where TRequest : IUnitOfWorkCommand
    {
        public override async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken = default)
        {
            var response = await next();
            var unitOfWork = serviceProvider.GetRequiredKeyedService<IUnitOfWork>(request.GetModuleName());
            await unitOfWork.Commit(cancellationToken);
            return response;
        }
    }
}

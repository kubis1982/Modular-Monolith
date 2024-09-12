namespace Kubis1982.Shared.Pipelines
{
    using FluentValidation;
    using FluentValidation.Results;
    using MediatR;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    internal class ValidationPipelineBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators) : PipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        public override Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken = default)
        {
            List<ValidationFailure> errors = validators
                .Select(v => v.Validate(request))
                .SelectMany(result => result.Errors)
                .Where(error => error != null)
                .ToList();

            if (errors.Count != 0)
            {
                StringBuilder errorBuilder = new();
                foreach (ValidationFailure error in errors)
                {
                    errorBuilder.AppendLine(error.ErrorMessage);
                }
                throw new ValidationException(errorBuilder.ToString(), errors);
            }

            return next();
        }
    }
}

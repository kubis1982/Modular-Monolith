namespace ModularMonolith.Shared.Exceptions
{
    using Microsoft.AspNetCore.Diagnostics;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;
    using ModularMonolith.Shared.Persistance.Exceptions;
    using System;
    using System.Net;
    using System.Threading;
    using System.Threading.Tasks;

    internal class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            string message = $"Exception occurred: {exception.Message}";
            logger.LogError(exception, message);
            int statusCode;
            string title = exception.Message;
            string detail = exception.GetBaseException().Message;
            switch (exception)
            {
                case ForbiddenException:
                statusCode = (int)HttpStatusCode.Forbidden;
                break;
                case EntityNotFoundException:
                statusCode = (int)HttpStatusCode.NotFound;
                break;
                case UniqueConstraintException:
                statusCode = (int)HttpStatusCode.Conflict;
                break;
                case DbUpdateException:
                case AppException:
                statusCode = (int)HttpStatusCode.BadRequest;
                break;
                default:
                statusCode = (int)HttpStatusCode.InternalServerError;
                break;
            }
            ProblemDetails problemDetails = new()
            {
                Status = statusCode,
                Title = title,
                Detail = detail
            };
            httpContext.Response.StatusCode = statusCode;
            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
            return true;
        }
    }
}

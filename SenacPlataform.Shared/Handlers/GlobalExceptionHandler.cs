using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using SenacPlataform.Shared.Extensions;
using System.Web.Http.ExceptionHandling;

namespace SenacPlataform.Shared.Handlers;

public sealed class GlobalExceptionHandler(IConfiguration configuration) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        var exceptionConfig = configuration.SNGetExceptionConfig();

        var result = new
        {
            Status = StatusCodes.Status500InternalServerError,
            Title = "Server error",
            Detail = exception.Message,
            exceptionConfig.ShowStackTrace,
            StackTrace = exceptionConfig.ShowStackTrace ? exception.StackTrace : null
        };

        httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

        await httpContext.Response
            .WriteAsJsonAsync(result, cancellationToken);

        return true;
    }
}
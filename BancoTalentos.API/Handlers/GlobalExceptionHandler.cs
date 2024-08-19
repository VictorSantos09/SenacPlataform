using BancoTalentos.Domain.Config;
using BancoTalentos.Domain.Extensions;
using Microsoft.AspNetCore.Diagnostics;

namespace BancoTalentos.API.Handlers;

internal sealed class GlobalExceptionHandler(IConfiguration configuration) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        var exceptionConfig = configuration.GetExceptionConfig();

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
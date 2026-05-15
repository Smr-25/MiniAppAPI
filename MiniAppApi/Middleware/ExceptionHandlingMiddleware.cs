using System.Net;
using MiniAppApi.Exceptions;

namespace MiniAppApi.Middleware;

public class ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
{
    private readonly ILogger<ExceptionHandlingMiddleware> _logger = logger;

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception exception)
        {
            await HandleExceptionAsync(context, exception);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        var response = new { message = exception.Message };

        switch (exception)
        {
            case EntityNotFoundException:
                context.Response.StatusCode = StatusCodes.Status404NotFound;
                break;
            case ApplicationException:
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                break;
            default:
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                break;
        }

        return context.Response.WriteAsJsonAsync(response);
    }
}


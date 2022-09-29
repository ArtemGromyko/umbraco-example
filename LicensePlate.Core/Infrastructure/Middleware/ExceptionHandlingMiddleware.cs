using System.Text.Json;
using LicensePlate.Core.Abstraction;
using Microsoft.AspNetCore.Http;
using Serilog;

namespace LicensePlate.Core.Infrastructure.Middleware;

public sealed class ExceptionHandlingMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (HttpExeptionAbstract e)
        {
            await HandleExceptionAsync(context, e, e.StatusCode);
        }
        catch (Exception e)
        {
            await HandleExceptionAsync(context, e, StatusCodes.Status500InternalServerError);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext httpContext, Exception exception, int statusCode)
    {
        Log.Error("{Exaption}", exception.Message);

        httpContext.Response.ContentType = "application/json";
        httpContext.Response.StatusCode = statusCode;

        var response = new
        {
            Error = exception.Message
        };

        await httpContext.Response.WriteAsync(JsonSerializer.Serialize(response));
    }
}
using LicensePlate.Core.Infrastructure.Middleware;
using Microsoft.AspNetCore.Builder;

namespace LicensePlate.Core.Infrastructure.Extensions;

public static class MiddlwareExtensions
{
    public static IApplicationBuilder UseUmbracoApiAutorization(this IApplicationBuilder app)
    {
        return app.UseMiddleware<UmbracoApiAutentificationMiddlware>();
    }

    public static IApplicationBuilder UseExceptionHandling(this IApplicationBuilder app)
    {
        return app.UseMiddleware<ExceptionHandlingMiddleware>();
    }
}

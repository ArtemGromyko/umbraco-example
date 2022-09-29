using System.Security.Claims;
using LicensePlate.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace LicensePlate.Core.Infrastructure.Middleware;

public class UmbracoApiAutentificationMiddlware : IMiddleware
{
    private readonly IJwtTokenService _jwtTokenService;

    public UmbracoApiAutentificationMiddlware(IJwtTokenService jwtTokenService)
    {
        _jwtTokenService = jwtTokenService;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        HttpRequest request = context.Request;

        StringValues authorizationHeader = request.Headers.Authorization;
        string token = authorizationHeader.FirstOrDefault(x => x.Contains("Bearer"));

        if (token is null)
        {
            await next(context);
            return;
        }

        token = token.ReplaceFirst("Bearer ", string.Empty);
        ClaimsPrincipal principal = _jwtTokenService.Read(token); //TODO: Check this validate

        if (principal is not null)
        {
            context.SetPrincipalForRequest(principal);
        }

        await next(context);
    }
}

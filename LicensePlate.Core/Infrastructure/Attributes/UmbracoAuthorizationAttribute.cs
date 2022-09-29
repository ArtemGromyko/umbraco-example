using System.Security.Claims;
using LicensePlate.Core.Interfaces;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;

namespace LicensePlate.Core.Infrastructure.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
public class UmbracoAuthorizationAttribute : ActionFilterAttribute, IAuthorizationFilter
{
    private readonly IJwtTokenService _jwtTokenService;

    public UmbracoAuthorizationAttribute(IJwtTokenService tokenService)
    {
        _jwtTokenService = tokenService;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var request = context.HttpContext.Request;

        // Parse the Authentication header
        StringValues authorizationHeader = request.Headers.Authorization;
        bool isBearer = authorizationHeader.Contains("bearer");

        if (isBearer)
            return;

        // Extract the principal from the header
        string token = authorizationHeader.FirstOrDefault();
        ClaimsPrincipal principal = _jwtTokenService.Read(token); //TODO: Check this validate

        if (principal is null)
            return;

        // Set the current principal
        context.HttpContext.SetPrincipalForRequest(principal);
    }
}
using System.Security.Claims;

namespace LicensePlate.Core.Infrastructure.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static string FindUserName(this ClaimsPrincipal principal)
    {
        return principal.Identity.GetUserName();
    }

    public static string FindUserId(this ClaimsPrincipal principal)
    {
        return principal.Identity.GetUserId();
    }
}

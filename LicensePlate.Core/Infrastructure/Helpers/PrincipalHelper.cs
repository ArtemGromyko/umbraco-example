using System.Security.Claims;
using LicensePlate.Core.Interfaces;

namespace LicensePlate.Core.Infrastructure.Helpers;

public static class PrincipalHelper
{
    public static async ValueTask<bool> ValidatePrincipal(ClaimsPrincipal principal, IUserService userService)
    {
        // Make sure principal isn't null
        if (principal == null)
            return false;

        // Make sure identity is authenticated
        if (!principal.Identity.IsAuthenticated)
            return false;

        // Make sure username is present
        var username = principal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;
        if (string.IsNullOrWhiteSpace(username))
            return false;

        // Make sure username is valid
        bool isValidUser = await userService.ValidateUser(username);
        if (!isValidUser)
            return false;

        return true;
    }
}

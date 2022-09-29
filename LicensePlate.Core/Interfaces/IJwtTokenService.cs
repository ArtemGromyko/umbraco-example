using System.Security.Claims;

namespace LicensePlate.Core.Interfaces;

public interface IJwtTokenService
{
    string Create(ClaimsIdentity identity);

    ClaimsPrincipal Read(string token);
}
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using LicensePlate.Core.Interfaces;
using LicensePlate.Core.Infrastructure.Extensions;

namespace LicensePlate.Core.Services;

public class JwtTokenService : IJwtTokenService
{
    private readonly IJwtTokenOptions _options;

    public JwtTokenService(IJwtTokenOptions options)
    {
        _options = options;
    }

    private string Secret => _options.SymmetricKey;

    private SymmetricSecurityKey SymmetricSecurityKey => Secret.ConvertToSymmetricSecurityKey();

    public string Create(ClaimsIdentity identity)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        var now = DateTime.UtcNow;
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = identity,
            IssuedAt = now,
            NotBefore = now,
            Expires = now.AddMinutes(_options.AccessTokenLifeTime),
            SigningCredentials = new SigningCredentials(SymmetricSecurityKey,
                SecurityAlgorithms.HmacSha256Signature,
                SecurityAlgorithms.Sha256Digest)
        };

        SecurityToken stoken = tokenHandler.CreateToken(tokenDescriptor);
        string token = tokenHandler.WriteToken(stoken);

        return token;
    }

    public ClaimsPrincipal Read(string token)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            if (tokenHandler.ReadToken(token) is not JwtSecurityToken jwtToken)
            {
                return null;
            }

            var validationParameters = new TokenValidationParameters()
            {
                RequireExpirationTime = true,
                ValidateIssuer = false,
                ValidateAudience = false,
                IssuerSigningKey = SymmetricSecurityKey,
                ClockSkew = TimeSpan.Zero
            };

            ClaimsPrincipal principal = tokenHandler.ValidateToken(token, validationParameters, out SecurityToken securityToken);

            return principal;
        }
        catch
        {
            return null;
        }
    }
}
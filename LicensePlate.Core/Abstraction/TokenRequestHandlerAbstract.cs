using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using LicensePlate.Core.Interfaces;
using MediatR;

namespace LicensePlate.Core.Abstraction;

public abstract class TokenRequestHandlerAbstract<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : ITokenResponse, new()
{
    private readonly IJwtTokenService _jwtTokenService;
    private readonly IRefreshTokenService _refreshTokenService;
    private readonly IUserService _userService;

    protected TokenRequestHandlerAbstract(IJwtTokenService jwtTokenService,
                                          IRefreshTokenService refreshTokenService,
                                          IUserService userService)
    {
        _jwtTokenService = jwtTokenService;
        _refreshTokenService = refreshTokenService;
        _userService = userService;
    }

    public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);

    protected async Task<TResponse> GenerateTokenResponseAsync(string username, string deviceId, int accessTokenLifeTime = 20)
    {
        ClaimsIdentity identity = await GenerateClaimsIdentity(username);

        string accessToken = _jwtTokenService.Create(identity);
        string userId = identity.FindFirstValue(JwtRegisteredClaimNames.NameId);
        string refreshTokenId = await _refreshTokenService.CreateAsync(accessToken, userId, deviceId);

        var response = new TResponse
        {
            AccessToken = accessToken,
            RefreshToken = refreshTokenId,
            TokenType = "bearer",
            ExpiresIn = accessTokenLifeTime * 60
        };

        return response;
    }

    private async Task<ClaimsIdentity> GenerateClaimsIdentity(string username)
    {
        var identity = new ClaimsIdentity("OAuth");

        IEnumerable<Claim> claims = await _userService.GetUserClaimsAsync(username);
        identity.AddClaims(claims);
        return identity;
    }
}

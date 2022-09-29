using LicensePlate.Core.Abstraction;
using LicensePlate.Core.Commands.Requests;
using LicensePlate.Core.Commands.Responses;
using LicensePlate.Core.Infrastructure.Exeptions;
using LicensePlate.Core.Interfaces;
using LicensePlate.Core.Models;

namespace LicensePlate.Core.Commands.Handlers;

public class RefreshRequestHandler : TokenRequestHandlerAbstract<RefreshRequest, RefreshResponse>
{
    private readonly IRefreshTokenService _refreshTokenService;
    private readonly IOAuthOptions _oauthOptions;
    private readonly IUserService _userService;

    public RefreshRequestHandler(IRefreshTokenService refreshTokenService,
                                IJwtTokenService jwtTokenService,
                                IOAuthOptions oauthOptions,
                                IUserService userService) : base(jwtTokenService, refreshTokenService, userService)
    {
        _refreshTokenService = refreshTokenService;
        _oauthOptions = oauthOptions;
        _userService = userService;
    }

    public override async Task<RefreshResponse> Handle(RefreshRequest request, CancellationToken cancellationToken)
    {
        RefreshToken refreshToken = await _refreshTokenService.GetByToken(request.RefreshToken.GenerateHash());

        if (refreshToken != null)
        {
            if (refreshToken.ExpireDate <= DateTime.UtcNow)
            {
                await _refreshTokenService.DeleteByToken(request.RefreshToken.GenerateHash());
                throw new UnauthorizedExeption("The refresh token has expired");
            }

            if (!refreshToken.DeviceId.IsNullOrWhiteSpace() && refreshToken.DeviceId != request.DeviceId)
            {
                throw new UnauthorizedExeption("Refresh token is associated with a different device");
            }

            string username = await _userService.GetUserNameById(refreshToken.UserId);
            return await GenerateTokenResponseAsync(username, refreshToken.DeviceId, _oauthOptions.AccessTokenLifeTime);
        }

        throw new RefreshTokenIsNotFoundExeption(request.RefreshToken);
    }
}

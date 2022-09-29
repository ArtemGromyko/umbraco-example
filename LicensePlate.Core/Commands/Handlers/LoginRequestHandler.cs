using LicensePlate.Core.Interfaces;
using LicensePlate.Core.Commands.Requests;
using LicensePlate.Core.Commands.Responses;
using LicensePlate.Core.Abstraction;
using LicensePlate.Core.Infrastructure.Exeptions;

namespace LicensePlate.Core.Commands.Handlers;

public class LoginRequestHandler : TokenRequestHandlerAbstract<LoginRequest, LoginResponse>
{
    private readonly IOAuthOptions _oauthOptions;
    private readonly IUserService _userService;

    public LoginRequestHandler(IUserService userService,
                               IJwtTokenService jwtTokenService,
                               IOAuthOptions oauthOptions,
                               IRefreshTokenService refreshTokenService) : base(jwtTokenService, refreshTokenService, userService)
    {
        _userService = userService;
        _oauthOptions = oauthOptions;
    }

    public override async Task<LoginResponse> Handle(LoginRequest request, CancellationToken cancellationToken)
    {
        bool isValidCredentials = await _userService.ValidateUser(request.UserName, request.Password);
        if (isValidCredentials)
        {
            return await GenerateTokenResponseAsync(request.UserName, request.DeviceId, _oauthOptions.AccessTokenLifeTime);
        }

        throw new FailedLoginExeption();
    }
}
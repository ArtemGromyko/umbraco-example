using LicensePlate.Core.Abstraction;
using LicensePlate.Core.Commands.Requests;
using LicensePlate.Core.Commands.Responses;
using LicensePlate.Core.Infrastructure.Exeptions;
using LicensePlate.Core.Interfaces;
using LicensePlate.Core.Models;

namespace LicensePlate.Core.Commands.Handlers
{
    public class RegistrationRequestHandler : TokenRequestHandlerAbstract<RegistrationRequest, RegistrationResponse>
    {
        private readonly IUserService _userService;
        private readonly IOAuthOptions _oAuthOptions;

        public RegistrationRequestHandler(IJwtTokenService jwtTokenService,
                                          IRefreshTokenService refreshTokenService,
                                          IUserService userService,
                                          IOAuthOptions oAuthOptions) : base(jwtTokenService, refreshTokenService, userService)
        {
            _userService = userService;
            _oAuthOptions = oAuthOptions;
        }

        public override async Task<RegistrationResponse> Handle(RegistrationRequest request, CancellationToken cancellationToken)
        {
            bool isExistedEmail = await _userService.IsExistedEmail(request.Email);
            if (isExistedEmail)
            {
                throw new ExistedEmailExeption(request.Email);
            }

            bool isExisetUserName = await _userService.IsExistedUserName(request.UserName);
            if (isExisetUserName)
            {
                throw new ExistedUsernameExeption(request.UserName);
            }

            var user = new UserModel
            {
                Name = request.Name,
                Email = request.Email,
                UserName = request.UserName,
                IsApproved = true
            };

            var isSuccess = await _userService.CreateUserAsync(user, request.Password);
            if (isSuccess)
            {
                return await GenerateTokenResponseAsync(request.UserName, request.DeviceId, _oAuthOptions.AccessTokenLifeTime);
            }

            throw new InternalExeption("Unseccessfull registration request");
        }
    }
}
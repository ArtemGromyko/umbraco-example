using LicensePlate.Core.Commands.Requests;
using LicensePlate.Core.Infrastructure.Exeptions;
using LicensePlate.Core.Interfaces;
using LicensePlate.Core.Models;
using MediatR;

namespace LicensePlate.Core.Commands.Handlers
{
    public class RevokeRequestHandler : IRequestHandler<RevokeRequest>
    {
        private readonly IRefreshTokenService _refreshTokenService;

        public RevokeRequestHandler(IRefreshTokenService refreshTokenService)
        {
            _refreshTokenService = refreshTokenService;
        }

        public async Task<Unit> Handle(RevokeRequest request, CancellationToken cancellationToken)
        {
            string hashedRefreshToken = request.RefreshToken.GenerateHash();
            RefreshToken token = await _refreshTokenService.GetByToken(hashedRefreshToken);

            if (token is not null)
            {
                if (token.ExpireDate <= DateTime.UtcNow)
                {
                    await _refreshTokenService.DeleteByToken(hashedRefreshToken);
                    throw new UnauthorizedExeption("The refresh token has expired");
                }

                if (!token.DeviceId.IsNullOrWhiteSpace() && token.DeviceId != request.DeviceId)
                {
                    throw new UnauthorizedExeption("Refresh token is associated with a different device");
                }

                await _refreshTokenService.DeleteByToken(hashedRefreshToken);
                return Unit.Value;
            }

            throw new RefreshTokenIsNotFoundExeption(request.RefreshToken);
        }
    }
}
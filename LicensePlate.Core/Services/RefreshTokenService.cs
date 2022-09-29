using LicensePlate.Core.Interfaces;
using LicensePlate.Core.Models;

namespace LicensePlate.Core.Services;

public class RefreshTokenService : IRefreshTokenService
{
    private readonly IRefreshTokenRepository<RefreshToken> _refreshTokenRepository;
    private readonly IOAuthOptions _oauthOptions;

    public RefreshTokenService(IRefreshTokenRepository<RefreshToken> refreshTokenRepository,
                               IOAuthOptions oauthOptions)
    {
        _refreshTokenRepository = refreshTokenRepository;
        _oauthOptions = oauthOptions;
    }

    public async Task<string> CreateAsync(string accessToken, string userId, string deviceId)
    {
        string refreshTokenId = Guid.NewGuid().ToString("n");

        var token = new RefreshToken
        {
            Token = refreshTokenId.GenerateHash(),
            JwtId = accessToken,
            CreationDate = DateTime.UtcNow,
            ExpireDate = DateTime.UtcNow.AddMinutes(_oauthOptions.RefreshTokenLifeTime),
            UserId = userId,
            DeviceId = deviceId.IsNullOrWhiteSpace() ? null : deviceId,
        };

        await _refreshTokenRepository.RemoveByUserId(userId, deviceId);
        await _refreshTokenRepository.AddRefreshToken(token);
        return refreshTokenId;
    }

    public async Task<RefreshToken> GetByToken(string token)
    {
        return await _refreshTokenRepository.FindRefreshToken(token);
    }

    public async Task DeleteByToken(string refreshTokenId)
    {
        await _refreshTokenRepository.RemoveByToken(refreshTokenId);
    }

    public async Task DeleteByUserId(string userId, string deviceId)
    {
        await _refreshTokenRepository.RemoveByUserId(userId, deviceId);
    }
}

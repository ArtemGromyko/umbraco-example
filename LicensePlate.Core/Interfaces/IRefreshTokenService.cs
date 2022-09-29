using LicensePlate.Core.Models;

namespace LicensePlate.Core.Interfaces;

public interface IRefreshTokenService
{
    Task<string> CreateAsync(string accessToken, string userId, string deviceId);
    Task DeleteByToken(string refreshTokenId);
    Task DeleteByUserId(string userId, string deviceId);
    Task<RefreshToken> GetByToken(string token);
}

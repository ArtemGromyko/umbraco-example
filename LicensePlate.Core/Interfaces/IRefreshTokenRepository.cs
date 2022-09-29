namespace LicensePlate.Core.Interfaces;

public interface IRefreshTokenRepository<T>
{
    Task AddRefreshToken(T token);

    Task RemoveByToken(string refreshTokenId);

    Task RemoveByUserId(string clientId, string deviceId);

    Task<T> FindRefreshToken(string refreshTokenId);
}
using LicensePlate.Core.Interfaces;
using LicensePlate.Core.Models;
using NPoco;

namespace LicensePlate.Core.Data;

public class UmbracoDbRefreshTokenRepository : IRefreshTokenRepository<RefreshToken>
{
    private readonly IDatabase _database;

    public UmbracoDbRefreshTokenRepository(IDatabase database)
    {
        _database = database;
    }

    public async Task AddRefreshToken(RefreshToken token)
    {
        await _database.SaveAsync(token);
    }

    public async Task<RefreshToken> FindRefreshToken(string refreshToken)
    {
        return await _database.SingleOrDefaultAsync<RefreshToken>(
            "SELECT * FROM [RefreshToken] WHERE [Token] = @0",
            refreshToken);
    }

    public async Task RemoveByToken(string refreshTokenId)
    {
        await _database.ExecuteAsync(
            "DELETE FROM [RefreshToken] WHERE [Token] = @0",
            refreshTokenId);
    }

    public async Task RemoveByUserId(string userId, string deviceId)
    {
        await _database.ExecuteAsync(
            "DELETE FROM [RefreshToken] WHERE [UserId] = @0 AND [DeviceId] = @1",
            userId,
            deviceId);
    }
}

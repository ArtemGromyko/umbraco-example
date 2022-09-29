namespace LicensePlate.Core.Interfaces;

public interface IJwtTokenOptions
{
    string SymmetricKey { get; set; }

    string AllowedOrigin { get; set; }

    int AccessTokenLifeTime { get; set; }

    int RefreshTokenLifeTime { get; set; }

    bool AllowInsecureHttp { get; set; }
}
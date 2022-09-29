namespace LicensePlate.Core.Interfaces;

public interface ITokenResponse
{
    string AccessToken { get; set; }
    string TokenType { get; set; }
    string RefreshToken { get; set; }
    int ExpiresIn { get; set; }
}

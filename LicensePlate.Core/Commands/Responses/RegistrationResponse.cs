using LicensePlate.Core.Interfaces;

namespace LicensePlate.Core.Commands.Responses;

public class RegistrationResponse : ITokenResponse
{
    public string AccessToken { get; set; }
    public string TokenType { get; set; }
    public string RefreshToken { get; set; }
    public int ExpiresIn { get; set; }
}
